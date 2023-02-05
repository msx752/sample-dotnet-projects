using AutoMapper;
using Cart.Database;
using Cart.Database.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SampleProject.Contract.Payment;
using SampleProject.Contract.Payment.Cart;

namespace SampleProject.Cart.API.Consumers
{
    public class CartEntityMessageConsumer :
            IConsumer<CartEntityRequestMessage>
    {
        private readonly ILogger<CartEntityMessageConsumer> _logger;
        private readonly IMapper mapper;
        private readonly IServiceProvider provider;

        public CartEntityMessageConsumer(
            ILogger<CartEntityMessageConsumer> logger
            , IMapper mapper
            , IServiceProvider provider
            )
        {
            _logger = logger;
            this.mapper = mapper;
            this.provider = provider;
        }

        public async Task Consume(ConsumeContext<CartEntityRequestMessage> context)
        {
            using (var scope = provider.CreateScope())
            using (var repository = scope.ServiceProvider.GetRequiredService<IDbContextFactory<CartDbContext>>().CreateRepository())
            {
                var entity = repository
                    .Where<CartEntity>(f =>
                        f.UserId == context.Message.ActivityUserId
                        && f.Id == context.Message.CartId
                    )
                    .Include(f => f.Items)
                    .FirstOrDefault();

                CartEntityResponseMessage cartEntityResponseMessage = null;
                if (entity == null)
                {
                    cartEntityResponseMessage = new();
                    cartEntityResponseMessage.BusErrorMessage = $"cart not found for selected user.";
                }
                else if (entity.Items.Count == 0)
                {
                    cartEntityResponseMessage = new();
                    cartEntityResponseMessage.BusErrorMessage = $"cart is empty.";
                }
                else
                {
                    cartEntityResponseMessage = mapper.Map<CartEntityResponseMessage>(entity);
                }

                _logger.LogInformation($"{nameof(CartEntity)} (id={entity?.Id}) is sent.");
                await context.RespondAsync(cartEntityResponseMessage);
            }
        }
    }
}