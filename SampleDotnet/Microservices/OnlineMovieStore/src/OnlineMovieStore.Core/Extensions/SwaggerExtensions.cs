using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SampleProject.Core.Extensions;
using SampleProject.Core.Filters;
using SampleProject.Core.Model.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SampleProject.Core.Extensions
{
    public static class SwaggerExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen((sgo) =>
            {
                sgo.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. (put without 'Bearer ')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                });
                sgo.OperationFilter<SwaggerAuthOperationFilter>();

                var xmls = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml");
                foreach (var xmlFileLocation in xmls)
                {
                    try
                    {
                        sgo.IncludeXmlComments(xmlFileLocation);
                    }
                    catch (Exception e)
                    {
                    }
                }
            });
            return services;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c => c.SerializeAsV2 = false);
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", Assembly.GetEntryAssembly().FullName.Split(',')[0]);
            });

            return app;
        }
    }
}