using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using netCoreAPI.Core.ApplicationService;
using netCoreAPI.Data.Migrations;
using netCoreAPI.Model.Tables;
using netCoreAPI.Model.ViewModels;
using netCoreAPI.Static.DbSeed;
using netCoreAPI.Static.Services;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider svc)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            //we dont need to call 'IMyRepository' in here to update database because 'IUnitOfWork' does my lightweight works
            MyContextSeed.SeedData(svc.GetRequiredService<IUnitOfWork>());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //models are binded by auto mapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Personal, PersonalViewModel>();
                cfg.CreateMap<PersonalViewModel, Personal>();
            });
            configuration.AssertConfigurationIsValid();

            services.AddDbContext<MyContext>(opt => opt.UseInMemoryDatabase(databaseName: "NetCoreApiDatabase"), ServiceLifetime.Transient)//not sql-server, not mysql but IN-MEMORY DATABASE (NO DATABASE MIGRATION AND UPDATE-DATABASE)
                    .AddTransient<IUnitOfWork, UnitOfWork>()
                    .AddTransient<IMyRepository, MyRepository>()
                    .AddControllers()
                        /// be sure the same value with below of the specified configuration, to not engage with type of any serialization error
                        /// <seealso cref="namespace:CustomImageProvider.Tests.MainControllerTests.jsonSerializerSettings"/>
                        .AddNewtonsoftJson((o) =>
                        {
                            o.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                            o.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None;
                        })
                    .Services.AddSingleton<IMapper>(configuration.CreateMapper());
        }
    }
}