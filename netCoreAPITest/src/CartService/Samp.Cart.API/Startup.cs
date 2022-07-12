using Microsoft.EntityFrameworkCore;
using Samp.Core.Extensions;
using Samp.Core.Model;
using Samp.Cart;
using Samp.Cart.Database.Migrations;
using Samp.Cart.Database;

namespace Samp.Cart.API
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

            var IdentityContext = new DbContextParameter<CartDbContext, CartDbContextSeed>((provider, opt) =>
                    opt.UseInMemoryDatabase(databaseName: nameof(CartDbContext)).EnableSensitiveDataLogging());

            services.AddCustomDbContext(IdentityContext);
        }
    }
}