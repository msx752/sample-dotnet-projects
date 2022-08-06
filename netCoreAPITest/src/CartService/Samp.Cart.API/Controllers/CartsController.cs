using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samp.Basket.API.Models.Dtos;
using Samp.Basket.Database.Entities;
using Samp.Basket.Database.Migrations;
using Samp.Cart.API.Models.Dtos;
using Samp.Cart.API.Models.Requests;
using Samp.Cart.Database.Entities;
using Samp.Contract;
using Samp.Contract.Cart.Movie;
using Samp.Contract.Cart.Requests;
using Samp.Core.Interfaces.Repositories;
using Samp.Core.Model.Base;
using Samp.Core.Results;

namespace Samp.Cart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : BaseController
    {
        private readonly ISharedRepository<CartDbContext> repository;
        private readonly IMessageBus messageBus;

        public CartsController(
            IMapper mapper
            , ISharedRepository<CartDbContext> repository
            , IMessageBus messageBus
            )
            : base(mapper)
        {
            this.repository = repository;
            this.messageBus = messageBus;
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            var entity = repository.Table<CartEntity>()
                .Where(f => f.UserId == LoggedUserId)
                .Include(f => f.Items.Where(x => !x.IsDeleted))
                .FirstOrDefault();

            if (entity == null)
            {
                entity = new CartEntity
                {
                    UserId = LoggedUserId,
                };
                repository.Table<CartEntity>().Insert(entity);
                repository.Commit(LoggedUserId);
            }
            return new OkResponse(mapper.Map<CartDto>(entity));
        }

        [HttpPost("{cartId}/Item")]
        public async Task<ActionResult> CartItemAdd([FromRoute] Guid cartId, [FromBody] CartItemAddModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            var entity = repository.Table<CartEntity>()
                .Where(f => f.UserId == LoggedUserId && f.Id == cartId)
                .FirstOrDefault();

            if (entity == null)
                return new NotFoundResponse($"cart not found: {cartId}");

            if (entity.Satus == Database.Enums.CartStatus.LockedOnPayment)
            {
                return new BadRequestResponse($"selected cart status is LOCKED due to payment process, please try again later");
            }

            if (entity.Satus == Database.Enums.CartStatus.Paid)
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
            repository.Table<CartItemEntity>().Insert(entityCartItem);
            repository.Commit(LoggedUserId);

            return new OkResponse(mapper.Map<CartItemDto>(entityCartItem));
        }

        [HttpDelete("{cartId}/Item/{cartItemId}")]
        public ActionResult CartRemove(Guid cartId, Guid cartItemId)
        {
            var entity = repository.Table<CartItemEntity>()
                .Where(f => f.CartId == cartId
                    && !f.Cart.IsDeleted
                    && f.Cart.UserId == LoggedUserId
                    && f.Id == cartItemId
                    )
                .Include(f => f.Cart)
                .FirstOrDefault();

            if (entity == null)
                return new NotFoundResponse($"selected item not found: {cartId}");

            if (entity.Cart.Satus == Database.Enums.CartStatus.LockedOnPayment)
            {
                return new BadRequestResponse($"selected cart status is LOCKED due to payment process, please try again later");
            }

            if (entity.Cart.Satus == Database.Enums.CartStatus.Paid)
            {
                return new BadRequestResponse($"selected cart status is PAID, no more items can be removed");
            }

            repository.Table<CartItemEntity>().Delete(entity);
            repository.Commit(LoggedUserId);

            return new OkResponse();
        }
    }
}