using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Samp.Core.Extensions;
using Samp.Core.RepositoryServices;
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
            services.AddGlobalStartupServices<netCoreAPISettings>(Configuration
                , new[] { typeof(MyContext) }
                , new[] { typeof(MyContextSeed) }
                );

            //not sql-server, not mysql but IN-MEMORY DATABASE (NO DATABASE MIGRATION AND UPDATE-DATABASE)
            services.AddDbContext<MyContext>(opt =>
                opt.UseInMemoryDatabase(databaseName: "NetCoreApiDatabase").EnableSensitiveDataLogging());
        }
    }
}