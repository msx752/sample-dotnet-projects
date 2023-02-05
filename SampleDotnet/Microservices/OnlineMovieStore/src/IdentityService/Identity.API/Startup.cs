using Identity.Database;
using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Extensions;
using SampleProject.Identity.API.Helpers;

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

            using (var scope = isp.CreateScope())
                DbInitializer.Initialize(scope.ServiceProvider.GetRequiredService<IDbContextFactory<IdentityDbContext>>());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGlobalStartupServices<IdentityApplicationSettings>(Configuration);

            var conStr = Configuration.GetConnectionString("DefaultConnection");
            var isUseDockerOcelot = Environment.GetEnvironmentVariable("USEDOCKEROCELOT"); //for the debugging purposes
            if (isUseDockerOcelot != null && isUseDockerOcelot == "true")
                conStr = conStr.Replace("127.0.0.1,1433", "mssqldb.container,1433");

            services.AddDbContextFactory<IdentityDbContext>(opt =>
                opt.UseSqlServer(conStr, s => s.EnableRetryOnFailure(5)));

            services.AddScoped<ITokenHelper, TokenHelper>();
        }
    }
}