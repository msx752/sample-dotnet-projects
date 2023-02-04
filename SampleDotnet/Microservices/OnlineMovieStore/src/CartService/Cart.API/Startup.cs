using Cart.Database;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SampleProject.Basket.Database.Migrations;
using SampleProject.Cart.API.Consumers;
using SampleProject.Contract;
using SampleProject.Contract.Extensions;
using SampleProject.Contract.Payment;
using SampleProject.Core.Extensions;
using SampleProject.Core.Interfaces.DbContexts;
using SampleProject.Core.Model;

namespace SampleProject.Cart.API
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
            services.AddGlobalStartupServices<CartApplicationSettings>(Configuration);

            services.AddDbContextFactory<CartDbContext>(opt =>
            {
                opt.UseInMemoryDatabase(databaseName: nameof(CartDbContext)).EnableSensitiveDataLogging();
            });

            services.AddCustomMassTransit(Configuration
            , (consumers) =>
            {
                consumers.AddConsumer<CartStatusMessageConsumer>();
                consumers.AddConsumer<CartEntityMessageConsumer>();
            }
            , (context, endpoints) =>
            {
                endpoints.ReceiveEndpoint(nameof(CartStatusRequestMessage), e =>
                {
                    e.ConfigureConsumer<CartStatusMessageConsumer>(context);
                });

                endpoints.ReceiveEndpoint(nameof(CartEntityRequestMessage), e =>
                {
                    e.ConfigureConsumer<CartEntityMessageConsumer>(context);
                });
            });
        }
    }
}