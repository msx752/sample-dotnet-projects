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
using SampleDotnet.RepositoryFactory.Interfaces;

namespace SampleProject.Cart.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : BaseController
    {
        private readonly IMessageBus _messageBus;
        private readonly IUnitOfWork _unitOfWork;

        public CartsController(
            IMapper mapper
            , IMessageBus messageBus
            , IUnitOfWork unitOfWork)
            : base(mapper)
        {
            this._messageBus = messageBus;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            using (var repository = _unitOfWork.CreateRepository<CartDbContext>())
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
                    await repository.InsertAsync(entity);
                }

                await _unitOfWork.SaveChangesAsync();

                return new OkResponse(mapper.Map<CartDto>(entity));
            }
        }

        [HttpPost("{cartId}/Item")]
        public async Task<ActionResult> CartItemAdd([FromRoute] Guid cartId, [FromBody] CartItemAddModel model)
        {
            if (!ModelState.IsValid)
                return new BadRequestResponse(ModelState.Values.SelectMany(f => f.Errors).Select(f => f.ErrorMessage));

            using (var repository = _unitOfWork.CreateRepository<CartDbContext>())
            {
                var entity = await repository
                    .Where<CartEntity>(f => f.UserId == LoggedUserId && f.Id == cartId)
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
                await repository.InsertAsync(entityCartItem);

                await _unitOfWork.SaveChangesAsync();

                return new OkResponse(mapper.Map<CartItemDto>(entityCartItem));
            }
        }

        [HttpDelete("{cartId}/Item/{cartItemId}")]
        public async Task<ActionResult> CartRemove(Guid cartId, Guid cartItemId)
        {
            using (var repository = _unitOfWork.CreateRepository<CartDbContext>())
            {
                var entity = await repository
                    .Where<CartItemEntity>(f => f.CartId == cartId
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

                repository.Delete(entity);

                await _unitOfWork.SaveChangesAsync();

                return new OkResponse();
            }
        }
    }
}