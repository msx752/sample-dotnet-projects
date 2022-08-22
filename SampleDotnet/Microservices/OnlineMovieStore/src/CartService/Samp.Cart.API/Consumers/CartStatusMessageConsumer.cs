using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Samp.Basket.Database.Entities;
using Samp.Basket.Database.Migrations;
using Samp.Cart.Database.Enums;
using Samp.Contract.Cart.Movie;
using Samp.Contract.Cart.Requests;
using Samp.Contract.Payment;
using Samp.Contract.Payment.Cart;
using Samp.Core.Interfaces.Repositories;

namespace Samp.Cart.API.Consumers
{
    public class CartStatusMessageConsumer :
            IConsumer<CartStatusRequestMessage>
    {
        private readonly ILogger<CartStatusMessageConsumer> _logger;
        private readonly IMapper mapper;
        private readonly IServiceProvider provider;

        public CartStatusMessageConsumer(
            ILogger<CartStatusMessageConsumer> logger
            , IMapper mapper
            , IServiceProvider provider
            )
        {
            _logger = logger;
            this.mapper = mapper;
            this.provider = provider;
        }

        public async Task Consume(ConsumeContext<CartStatusRequestMessage> context)
        {
            using (var scope = provider.CreateScope())
            using (var repository = scope.ServiceProvider.GetRequiredService<IUnitOfWork<CartDbContext>>())
            {
                var entity = repository.Table<CartEntity>()
                    .Where(f =>
                            f.Id == context.Message.CartId
                            && f.UserId == context.Message.ActivityUserId
                            && !f.IsDeleted
                    )
                    .FirstOrDefault();

                CartStatusResponseMessage cartStatusResponseMessage = new CartStatusResponseMessage();
                cartStatusResponseMessage.ActivityId = context.Message.ActivityId;
                cartStatusResponseMessage.ActivityUserId = context.Message.ActivityUserId;

                if (entity == null)
                {
                    cartStatusResponseMessage.BusErrorMessage = $"cart not found: {context.Message.CartId}";
                }
                else if (Enum.TryParse<CartStatus>(context.Message.CartStatus, out CartStatus cartStatus))
                {
                    entity.Satus = cartStatus;
                    repository.Table<CartEntity>().Update(entity);
                    repository.SaveChanges(context.Message.ActivityUserId);
                }
                else
                {
                    cartStatusResponseMessage.BusErrorMessage = $"CartStatus not found: {context.Message.CartStatus}";
                }

                _logger.LogInformation($"{nameof(CartEntity)} (id={entity?.Id}) is sent.");
                await context.RespondAsync(cartStatusResponseMessage);
            }
        }
    }
}