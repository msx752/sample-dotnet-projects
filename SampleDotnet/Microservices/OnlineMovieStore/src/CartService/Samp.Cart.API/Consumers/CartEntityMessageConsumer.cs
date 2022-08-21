using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Samp.Basket.Database.Entities;
using Samp.Basket.Database.Migrations;
using Samp.Contract.Payment;
using Samp.Contract.Payment.Cart;
using Samp.Core.Interfaces.Repositories;

namespace Samp.Cart.API.Consumers
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
            using (var repository = scope.ServiceProvider.GetRequiredService<ISharedRepository<CartDbContext>>())
            {
                var entity = repository.Table<CartEntity>()
                    .Where(f =>
                        f.UserId == context.Message.ActivityUserId
                        && f.Id == context.Message.CartId
                    )
                    .Include(f => f.Items.Where(x => !x.IsDeleted))
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