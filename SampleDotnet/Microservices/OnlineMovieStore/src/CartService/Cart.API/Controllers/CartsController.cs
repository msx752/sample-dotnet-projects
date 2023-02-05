using AutoMapper;
using Cart.Database;
using Cart.Database.Entities;
using Cart.Database.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Basket.API.Models.Dtos;
using SampleProject.Cart.API.Models.Dtos;
using SampleProject.Cart.API.Models.Requests;
using SampleProject.Contract;
using SampleProject.Contract.Cart.Movie;
using SampleProject.Contract.Cart.Requests;
using SampleProject.Core.Model.Base;
using SampleProject.Result;

namespace SampleProject.Cart.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : BaseController
    {
        private readonly IMessageBus messageBus;
        private readonly IDbContextFactory<CartDbContext> _contextFactory;

        public CartsController(
            IMapper mapper
            , IMessageBus messageBus
            , IDbContextFactory<CartDbContext> contextFactory)
            : base(mapper)
        {
            this.messageBus = messageBus;
            this._contextFactory = contextFactory;
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            using (var repository = _contextFactory.CreateRepository())
            {
                var entity = repository
                    .Where<CartEntity>(f => f.UserId == LoggedUserId && f.Satus != CartStatus.Paid)
                    .Include(f => f.Items)
                    .FirstOrDefault();

                if (entity == null)
                {
                    entity = new CartEntity
                    {
                        UserId = LoggedUserId,
                    };
                    repository.Insert(entity);
                    repository.SaveChanges();
                }

                return new OkResponse(mapper.Map<CartDto>(entity));
            }
        }

        [HttpPost("{cartId}/Item")]
        public async Task<ActionResult> CartItemAdd([FromRoute] Guid cartId, [FromBody] CartItemAddModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            using (var repository = _contextFactory.CreateRepository())
            {
                var entity = repository
                    .Where<CartEntity>(f => f.UserId == LoggedUserId && f.Id == cartId)
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
                repository.Insert(entityCartItem);
                repository.SaveChanges();

                return new OkResponse(mapper.Map<CartItemDto>(entityCartItem));
            }
        }

        [HttpDelete("{cartId}/Item/{cartItemId}")]
        public ActionResult CartRemove(Guid cartId, Guid cartItemId)
        {
            using (var repository = _contextFactory.CreateRepository())
            {
                var entity = repository
                    .Where<CartItemEntity>(f => f.CartId == cartId
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

                repository.Delete(entity);
                repository.SaveChanges();

                return new OkResponse();
            }
        }
    }
}