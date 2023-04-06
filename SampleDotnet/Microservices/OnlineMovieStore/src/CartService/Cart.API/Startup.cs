using Cart.Database;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SampleDotnet.RepositoryFactory.Interfaces;
using SampleProject.Cart.API.Consumers;
using SampleProject.Contract.Extensions;
using SampleProject.Contract.Payment;
using SampleProject.Core.Extensions;

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

            using (var scope = isp.CreateScope())
                DbInitializer.Initialize(scope.ServiceProvider.GetRequiredService<IUnitOfWork>());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGlobalStartupServices<CartApplicationSettings>(Configuration);

            var conStr = Configuration.GetConnectionString("DefaultConnection");
            var isUseDockerOcelot = Environment.GetEnvironmentVariable("USEDOCKEROCELOT"); //for the debugging purposes
            if (isUseDockerOcelot != null && isUseDockerOcelot == "true")
                conStr = conStr.Replace("127.0.0.1,1433", "mssqldb.container,1433");

            services.AddDbContextFactoryWithUnitOfWork<CartDbContext>(opt =>
                opt.UseSqlServer(conStr));

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