using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using netCoreAPITest.Data.Migrations;
using netCoreAPITest.Data.Tables;
using netCoreAPITest.Models;

namespace netCoreAPITest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Personal, PersonalDto>();
            });
            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();

            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase(databaseName: "netCoreAPITestdb"));//not sql-server, not mysql but IN-MEMORY DATABASE (NO DATABASE MIGRATION AND UPDATE-DATABASE)
            services.AddScoped<ApiContext>();
            services.AddControllers();
            services.AddSingleton<IMapper>(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider svc)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            var context = svc.GetRequiredService<ApiContext>();
            await ApiContext.SeedData(context);//makes snync

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
