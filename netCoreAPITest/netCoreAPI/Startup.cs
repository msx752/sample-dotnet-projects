using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using netCoreAPI.Controllers;
using netCoreAPI.Core;
using netCoreAPI.Core.Interfaces.Repositories.Shared;
using netCoreAPI.Data.Migrations;
using netCoreAPI.OperationFilters;
using netCoreAPI.Static.AppSettings;
using netCoreAPI.Static.DbSeed;
using netCoreAPI.Static.Services;
using System;
using System.IO;
using System.Text;

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
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger(c => c.SerializeAsV2 = false);
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "netCoreAPI V1");
            });

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            //Seed Data
            using (var scope = isp.CreateScope())
            {
                MyContextSeed.SeedData(scope.ServiceProvider.GetRequiredService<ISharedConnection>());
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityMapper();
            services.Configure<ApplicationSettings>(Configuration);
            services.AddHttpContextAccessor();
            services.AddAuthentication((ao) => ao.DefaultChallengeScheme = ao.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, (options) =>
                        {
                            options.RequireHttpsMetadata = false;
                            options.TokenValidationParameters = new TokenValidationParameters()
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateIssuerSigningKey = true,
                                RequireExpirationTime = true,
                                ClockSkew = TimeSpan.Zero,
                                ValidAudience = Configuration["JWT:ValidAudience"],
                                ValidIssuer = Configuration["JWT:ValidIssuer"],
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                            };
                        });
            services.AddDbContext<MyContext>(opt => opt.UseInMemoryDatabase(databaseName: "NetCoreApiDatabase").EnableSensitiveDataLogging());//not sql-server, not mysql but IN-MEMORY DATABASE (NO DATABASE MIGRATION AND UPDATE-DATABASE)
            services.AddScoped<ISharedConnection, SharedConnection>();
            services.AddScoped<ISharedRepository, SharedRepository>();
            services.AddSwaggerGen((sgo) =>
            {
                sgo.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. (put without 'Bearer ')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });
                sgo.OperationFilter<SwaggerAuthOperationFilter>();

                var xmlFile = $"{typeof(_BaseController).Namespace}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                sgo.IncludeXmlComments(xmlPath);
            });
            services.AddControllers()
                    /// be sure the same value with below of the specified configuration, to not engage with type of any serialization error
                    /// <seealso cref="namespace:CustomImageProvider.Tests.MainControllerTests.jsonSerializerSettings"/>
                    .AddNewtonsoftJson((o) =>
                    {
                        o.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                        o.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None;
                    });
        }
    }
}