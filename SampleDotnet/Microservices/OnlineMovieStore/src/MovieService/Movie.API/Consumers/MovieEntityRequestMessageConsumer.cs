using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Movie.Database;
using Movie.Database.Entities;

using SampleProject.Contract.Cart.Movie;
using SampleProject.Contract.Cart.Requests;

namespace SampleProject.Movie.API.Consumers
{
    public class MovieEntityRequestMessageConsumer :
            IConsumer<MovieEntityRequestMessage>
    {
        private readonly ILogger<MovieEntityRequestMessageConsumer> _logger;
        private readonly IMapper mapper;
        private readonly IDbContextFactory<MovieDbContext> _dbContextFactory;

        public MovieEntityRequestMessageConsumer(
            ILogger<MovieEntityRequestMessageConsumer> logger
            , IMapper mapper
            , IDbContextFactory<MovieDbContext> dbContextFactory)
        {
            _logger = logger;
            this.mapper = mapper;
            _dbContextFactory = dbContextFactory;
        }

        public async Task Consume(ConsumeContext<MovieEntityRequestMessage> context)
        {
            using (var dbcontext = await _dbContextFactory.CreateDbContextAsync())

            {
                var movieEntity = await dbcontext.Movies
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == context.Message.ProductId);

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