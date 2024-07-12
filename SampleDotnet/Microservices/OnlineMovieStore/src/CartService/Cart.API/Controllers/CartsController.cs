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
using SampleDotnet.Result;


namespace SampleProject.Cart.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : BaseController
    {
        private readonly IMessageBus _messageBus;
        private readonly IDbContextFactory<CartDbContext> _dbContextFactory;

        public CartsController(
            IMapper mapper
            , IMessageBus messageBus
            , IDbContextFactory<CartDbContext> factoryCartDbContext)
            : base(mapper)
        {
            this._messageBus = messageBus;
            _dbContextFactory = factoryCartDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
            {
                var entity = dbContext.Baskets.Where(f => f.UserId == LoggedUserId && f.Satus != CartStatus.Paid)
                    .Include(f => f.Items)
                    .FirstOrDefault();

                if (entity == null)
                {
                    entity = new CartEntity
                    {
                        UserId = LoggedUserId,
                    };
                    await dbContext.Baskets.AddAsync(entity);
                }

                await dbContext.SaveChangesAsync();

                return new OkResponse(mapper.Map<CartDto>(entity));
            }
        }

        [HttpPost("{cartId}/Item")]
        public async Task<ActionResult> CartItemAdd([FromRoute] Guid cartId, [FromBody] CartItemAddModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
            {
                var entity = await dbContext.Baskets.Where(f => f.UserId == LoggedUserId && f.Id == cartId)
                    .FirstOrDefaultAsync();

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

                var movieEntityResponse = await _messageBus.Call<MovieEntityResponseMessage, MovieEntityRequestMessage>(new()
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
                await dbContext.BasketItems.AddAsync(entityCartItem);

                await dbContext.SaveChangesAsync();

                return new OkResponse(mapper.Map<CartItemDto>(entityCartItem));
            }
        }

        [HttpDelete("{cartId}/Item/{cartItemId}")]
        public async Task<ActionResult> CartRemove(Guid cartId, Guid cartItemId)
        {
            using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
            {
                var entity = await dbContext.BasketItems.Where(f => f.CartId == cartId
                        && f.Cart.UserId == LoggedUserId
                        && f.Id == cartItemId
                        )
                    .Include(f => f.Cart)
                    .FirstOrDefaultAsync();

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

                dbContext.BasketItems.Remove(entity);

                await dbContext.SaveChangesAsync();

                return new OkResponse();
            }
        }
    }
}