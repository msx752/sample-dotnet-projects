using MassTransit;
using Microsoft.EntityFrameworkCore;
using Samp.Contract;
using Samp.Contract.Extensions;
using Samp.Core.Extensions;
using Samp.Core.Model;
using Samp.Payment.Database;
using Samp.Payment.Database.Migrations;

namespace Samp.Payment.API
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
            services.AddGlobalStartupServices<PaymentApplicationSettings>(Configuration);

            var IdentityContext = new DbContextParameter<PaymentDbContext, PaymentDbContextSeed>((provider, opt) =>
                    opt.UseInMemoryDatabase(databaseName: nameof(PaymentDbContext)).EnableSensitiveDataLogging());

            services.AddCustomDbContext(IdentityContext);

            services.AddCustomMassTransit(Configuration, null, null);
        }
    }
}