using AutoMapper;
using MassTransit;
using SampleProject.Contract;
using SampleProject.Contract.Cart.Movie;
using SampleProject.Contract.Cart.Requests;
using SampleProject.Core.Interfaces.Repositories;
using SampleProject.Movie.Database.Entities;
using SampleProject.Movie.Database.Migrations;

namespace SampleProject.Movie.API.Consumers
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
            using (var repository = scope.ServiceProvider.GetRequiredService<IRepository<MovieDbContext>>())
            {
                var movieEntity = repository
                    .Find<MovieEntity>(keyValues: context.Message.ProductId);

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