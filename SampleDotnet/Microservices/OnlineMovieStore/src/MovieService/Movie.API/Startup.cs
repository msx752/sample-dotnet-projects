using MassTransit;
using Microsoft.EntityFrameworkCore;
using Movie.Database;
using SampleProject.Contract.Cart.Requests;
using SampleProject.Contract.Extensions;
using SampleProject.Core.Extensions;
using SampleProject.Core.Interfaces.DbContexts;
using SampleProject.Core.Model;
using SampleProject.Movie.API.Consumers;

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

            using (var scope = isp.CreateScope())
                DbInitializer.Initialize(scope.ServiceProvider.GetRequiredService<IDbContextFactory<MovieDbContext>>());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGlobalStartupServices<MovieApplicationSettings>(Configuration);

            var conStr = Configuration.GetConnectionString("DefaultConnection");
            var isUseDockerOcelot = Environment.GetEnvironmentVariable("USEDOCKEROCELOT"); //for the debugging purposes
            if (isUseDockerOcelot != null && isUseDockerOcelot == "true")
                conStr = conStr.Replace("127.0.0.1,1433", "mssqldb.container,1433");

            services.AddDbContextFactory<MovieDbContext>(opt =>
                opt.UseSqlServer(conStr, s => s.EnableRetryOnFailure(5)).EnableSensitiveDataLogging());

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