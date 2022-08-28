using MassTransit;
using Microsoft.EntityFrameworkCore;
using SampleProject.Contract;
using SampleProject.Contract.Extensions;
using SampleProject.Core.Extensions;
using SampleProject.Core.Model;
using SampleProject.Payment.Database;
using SampleProject.Payment.Database.Migrations;

namespace SampleProject.Payment.API
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

            var IdentityContext = new DbContextParameter<PaymentDbContext>((provider, opt) =>
                    opt.UseInMemoryDatabase(databaseName: nameof(PaymentDbContext)).EnableSensitiveDataLogging());

            services.AddCustomDbContext(IdentityContext);

            services.AddCustomMassTransit(Configuration, null, null);
        }
    }
}