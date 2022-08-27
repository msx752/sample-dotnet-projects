using Microsoft.EntityFrameworkCore;
using SampleProject.Auth.Database;
using SampleProject.Core.Extensions;
using SampleProject.Core.Model;
using SampleProject.Identity.API.Helpers;
using SampleProject.Identity.Core.Migrations;

namespace SampleProject.Identity.API
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
            services.AddGlobalStartupServices<IdentityApplicationSettings>(Configuration);

            var IdentityContext = new DbContextParameter<IdentityDbContext, IdentityContextSeed>((provider, opt) =>
                    opt.UseInMemoryDatabase(databaseName: nameof(IdentityDbContext)).EnableSensitiveDataLogging());

            services.AddCustomDbContext(IdentityContext);
            services.AddScoped<ITokenHelper, TokenHelper>();
        }
    }
}