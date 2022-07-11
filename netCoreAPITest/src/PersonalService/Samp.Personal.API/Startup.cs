using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Samp.Core.Extensions;
using Samp.Core.Model;
using Samp.Database.Personal;
using Samp.Database.Personal.Migrations;
using System;

namespace Samp.API.Personal
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
            var dbContext1 = new DbContextParameter<MyContext, MyContextSeed>((provider, opt) =>
                    opt.UseInMemoryDatabase(databaseName: "NetCoreApiDatabase").EnableSensitiveDataLogging());

            services.AddCustomDbContext(dbContext1);

            services.AddGlobalStartupServices<netCoreAPISettings>(Configuration);
        }
    }
}