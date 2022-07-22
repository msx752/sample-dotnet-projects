using AutoMapper;
using MassTransit;
using Samp.Contract.Cart.Requests;
using Samp.Contract.Cart.Responses;
using Samp.Core.Interfaces.Repositories;
using Samp.Movie.Database.Entities;
using Samp.Movie.Database.Migrations;

namespace Samp.Movie.API.Consumers
{
    public class MovieEntityRequestMessageConsumer :
            IConsumer<MovieEntityRequestMessage>
    {
        private readonly ILogger<MovieEntityRequestMessageConsumer> _logger;
        private readonly IServiceProvider serviceProvider;
        private readonly IMapper mapper;

        public MovieEntityRequestMessageConsumer(
            ILogger<MovieEntityRequestMessageConsumer> logger
            , IServiceProvider serviceProvider
            , IMapper mapper
            )
        {
            _logger = logger;
            this.serviceProvider = serviceProvider;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<MovieEntityRequestMessage> context)
        {
            using (var scope = serviceProvider.CreateScope())
            using (var repository = scope.ServiceProvider.GetRequiredService<ISharedRepository<MovieDbContext>>())
            {
                var movieEntity = repository.Table<MovieEntity>().Find(keyValues: context.Message.ProductId);

                await context.RespondAsync(movieEntity != null ? mapper.Map<MovieEntityResponseMessage>(movieEntity) : null);
            }
        }
    }
}