using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using netCoreAPI.Core.Extensions;
using netCoreAPI.Database;
using netCoreAPI.Database.Migrations;
using Samp.Core.RepositoryServices;
using System;

namespace netCoreAPI
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
            services.AddGlobalStartupServices<netCoreAPISettings>(Configuration, typeof(SharedConnection<MyContext>), typeof(SharedRepository<MyContext>), typeof(MyContextSeed<MyContext>));

            services.AddDbContext<MyContext>(opt => opt.UseInMemoryDatabase(databaseName: "NetCoreApiDatabase").EnableSensitiveDataLogging());//not sql-server, not mysql but IN-MEMORY DATABASE (NO DATABASE MIGRATION AND UPDATE-DATABASE)
            services.AddDbContextSeed(typeof(MyContextSeed<MyContext>));
        }
    }
}