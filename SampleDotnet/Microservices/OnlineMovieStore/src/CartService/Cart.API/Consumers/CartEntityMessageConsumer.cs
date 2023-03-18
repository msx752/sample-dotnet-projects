using AutoMapper;
using Cart.Database;
using Cart.Database.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SampleProject.Contract;
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
            , IServiceProvider provider)
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
                var entity = await repository
                    .Where<CartEntity>(f =>
                        f.UserId == context.Message.ActivityUserId
                        && f.Id == context.Message.CartId
                    )
                    .Include(f => f.Items)
                    .FirstOrDefaultAsync();

                CartEntityResponseMessage cartEntityResponseMessage = null;
                if (entity == null)
                {
                    cartEntityResponseMessage = new();
                    cartEntityResponseMessage.BusErrorMessage = $"cart not found for selected user.";

                    var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
                    await messageBus.Call<CartStatusResponseMessage, CartStatusRequestMessage>(new()
                    {
                        CartStatus = "Open",
                        CartId = context.Message.CartId,
                        ActivityUserId = context.Message.ActivityUserId,
                        ActivityId = context.Message.ActivityId,
                    });
                }
                else if (entity.Items.Count == 0)
                {
                    cartEntityResponseMessage = new();
                    cartEntityResponseMessage.BusErrorMessage = $"cart is empty.";

                    var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
                    await messageBus.Call<CartStatusResponseMessage, CartStatusRequestMessage>(new()
                    {
                        CartStatus = "Open",
                        CartId = context.Message.CartId,
                        ActivityUserId = context.Message.ActivityUserId,
                        ActivityId = context.Message.ActivityId,
                    });
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