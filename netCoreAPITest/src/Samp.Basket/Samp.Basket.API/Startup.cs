using Microsoft.EntityFrameworkCore;
using Samp.Basket.Database;
using Samp.Basket.Database.Migrations;
using Samp.Core.Extensions;
using Samp.Core.Model;

namespace Samp.Basket.API
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
            services.AddGlobalStartupServices<BasketApplicationSettings>(Configuration);

            var IdentityContext = new DbContextParameter<BasketDbContext, BasketDbContextSeed>((provider, opt) =>
                    opt.UseInMemoryDatabase(databaseName: nameof(BasketDbContext)).EnableSensitiveDataLogging());

            services.AddCustomDbContext(IdentityContext);
        }
    }
}