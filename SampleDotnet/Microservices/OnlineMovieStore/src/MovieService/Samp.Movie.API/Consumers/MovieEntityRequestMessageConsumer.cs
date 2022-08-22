using AutoMapper;
using MassTransit;
using Samp.Contract;
using Samp.Contract.Cart.Movie;
using Samp.Contract.Cart.Requests;
using Samp.Core.Interfaces.Repositories;
using Samp.Movie.Database.Entities;
using Samp.Movie.Database.Migrations;

namespace Samp.Movie.API.Consumers
{
    public class MovieEntityRequestMessageConsumer :
            IConsumer<MovieEntityRequestMessage>
    {
        private readonly ILogger<MovieEntityRequestMessageConsumer> _logger;
        private readonly IMapper mapper;
        private readonly IServiceProvider provider;

        public MovieEntityRequestMessageConsumer(
            ILogger<MovieEntityRequestMessageConsumer> logger
            , IMapper mapper
            , IServiceProvider provider
            )
        {
            _logger = logger;
            this.mapper = mapper;
            this.provider = provider;
        }

        public async Task Consume(ConsumeContext<MovieEntityRequestMessage> context)
        {
            using (var scope = provider.CreateScope())
            using (var repository = scope.ServiceProvider.GetRequiredService<IUnitOfWork<MovieDbContext>>())
            {
                var movieEntity = repository
                    .Table<MovieEntity>()
                    .Find(keyValues: context.Message.ProductId);

                MovieEntityResponseMessage responseMessage;
                if (movieEntity == null)
                {
                    responseMessage = new MovieEntityResponseMessage();
                    responseMessage.BusErrorMessage = $"movie not found: {context.Message.ProductId}";
                }
                else
                {
                    responseMessage = mapper.Map<MovieEntityResponseMessage>(movieEntity);
                }
                _logger.LogInformation($"{nameof(MovieEntity)} (id={movieEntity?.Id}) is sent.");
                await context.RespondAsync(responseMessage);
            }
        }
    }
}