using MassTransit;
using Microsoft.EntityFrameworkCore;
using SampleProject.Contract.Cart.Requests;
using SampleProject.Contract.Extensions;
using SampleProject.Core.Extensions;
using SampleProject.Core.Model;
using SampleProject.Movie.API.Consumers;
using SampleProject.Movie.Database;
using SampleProject.Movie.Database.Migrations;

namespace SampleProject.Movie.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider isp)
        {
            app.UseGlobalStartupConfigures(env);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGlobalStartupServices<MovieApplicationSettings>(Configuration);

            var IdentityContext = new DbContextParameter<MovieDbContext, MovieDbContextSeed>((provider, opt) =>
                    opt.UseInMemoryDatabase(databaseName: nameof(MovieDbContext)).EnableSensitiveDataLogging());

            services.AddCustomDbContext(IdentityContext);

            services.AddCustomMassTransit(Configuration
            , (consumers) =>
            {
                consumers.AddConsumer<MovieEntityRequestMessageConsumer>();
            }
            , (context, endpoints) =>
            {
                endpoints.ReceiveEndpoint(nameof(MovieEntityRequestMessage), e =>
                {
                    e.ConfigureConsumer<MovieEntityRequestMessageConsumer>(context);
                });
            });
        }
    }
}