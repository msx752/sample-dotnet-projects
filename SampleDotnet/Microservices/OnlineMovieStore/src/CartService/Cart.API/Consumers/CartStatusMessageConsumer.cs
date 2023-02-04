using AutoMapper;
using Cart.Database.Entities;
using Cart.Database.Enums;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SampleProject.Basket.Database.Migrations;
using SampleProject.Contract.Cart.Movie;
using SampleProject.Contract.Cart.Requests;
using SampleProject.Contract.Payment;
using SampleProject.Contract.Payment.Cart;
using SampleProject.Core.Interfaces.Repositories;

namespace SampleProject.Cart.API.Consumers
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
            using (var repository = scope.ServiceProvider.GetRequiredService<IDbContextFactory<CartDbContext>>().CreateRepository())
            {
                var entity = repository
                    .Where<CartEntity>(f =>
                            f.Id == context.Message.CartId
                            && f.UserId == context.Message.ActivityUserId
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
                    repository.Update(entity);
                    repository.SaveChanges();
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