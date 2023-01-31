using AutoMapper;
using Cart.Database.Entities;
using Cart.Database.Enums;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Basket.API.Models.Dtos;
using SampleProject.Basket.Database.Migrations;
using SampleProject.Cart.API.Models.Dtos;
using SampleProject.Cart.API.Models.Requests;
using SampleProject.Cart.Database.Entities;
using SampleProject.Contract;
using SampleProject.Contract.Cart.Movie;
using SampleProject.Contract.Cart.Requests;
using SampleProject.Core.Interfaces.Repositories;
using SampleProject.Core.Model.Base;
using SampleProject.Result;

namespace SampleProject.Cart.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : BaseController
    {
        private readonly IUnitOfWork<CartDbContext> _uow;
        private readonly IMessageBus messageBus;

        public CartsController(
            IMapper mapper
            , IUnitOfWork<CartDbContext> repository
            , IMessageBus messageBus
            )
            : base(mapper)
        {
            this._uow = repository;
            this.messageBus = messageBus;
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            var entity = _uow.Table<CartEntity>()
                .Where(f => f.UserId == LoggedUserId && f.Satus != CartStatus.Paid)
                .Include(f => f.Items)
                .FirstOrDefault();

            if (entity == null)
            {
                entity = new CartEntity
                {
                    UserId = LoggedUserId,
                };
                _uow.Table<CartEntity>().Insert(entity);
                _uow.SaveChanges();
            }
            return new OkResponse(mapper.Map<CartDto>(entity));
        }

        [HttpPost("{cartId}/Item")]
        public async Task<ActionResult> CartItemAdd([FromRoute] Guid cartId, [FromBody] CartItemAddModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            var entity = _uow.Table<CartEntity>()
                .Where(f => f.UserId == LoggedUserId && f.Id == cartId)
                .FirstOrDefault();

            if (entity == null)
                return new NotFoundResponse($"cart not found: {cartId}");

            if (entity.Satus == CartStatus.LockedOnPayment)
            {
                return new BadRequestResponse($"selected cart status is LOCKED due to payment process, please try again later");
            }

            if (entity.Satus == CartStatus.Paid)
            {
                return new BadRequestResponse($"selected cart status is PAID, no more items can be added");
            }

            var movieEntityResponse = await messageBus.Call<MovieEntityResponseMessage, MovieEntityRequestMessage>(new()
            {
                ProductId = model.ProductId,
                ProductDatabase = model.ProductDatabase,
                ActivityUserId = LoggedUserId,
            });

            if (movieEntityResponse.Message.BusErrorMessage != null)
                return new NotFoundResponse(movieEntityResponse.Message.BusErrorMessage);

            var entityCartItem = new CartItemEntity()
            {
                ProductId = model.ProductId,
                ProductDatabase = model.ProductDatabase,
                Title = movieEntityResponse.Message.Title,
                CartId = cartId,
                SalesPriceCurrency = "usd",
                SalesPrice = movieEntityResponse.Message.UsdPrice,
            };
            _uow.Table<CartItemEntity>().Insert(entityCartItem);
            _uow.SaveChanges();

            return new OkResponse(mapper.Map<CartItemDto>(entityCartItem));
        }

        [HttpDelete("{cartId}/Item/{cartItemId}")]
        public ActionResult CartRemove(Guid cartId, Guid cartItemId)
        {
            var entity = _uow.Table<CartItemEntity>()
                .Where(f => f.CartId == cartId
                    && f.Cart.UserId == LoggedUserId
                    && f.Id == cartItemId
                    )
                .Include(f => f.Cart)
                .FirstOrDefault();

            if (entity == null)
                return new NotFoundResponse($"selected item not found: {cartId}");

            if (entity.Cart.Satus == CartStatus.LockedOnPayment)
            {
                return new BadRequestResponse($"selected cart status is LOCKED due to payment process, please try again later");
            }

            if (entity.Cart.Satus == CartStatus.Paid)
            {
                return new BadRequestResponse($"selected cart status is PAID, no more items can be removed");
            }

            _uow.Table<CartItemEntity>().Delete(entity);
            _uow.SaveChanges();

            return new OkResponse();
        }
    }
}