using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Samp.Core.Extensions;
using Samp.Core.Filters;
using Samp.Core.Model.Base;
using System;
using System.Linq;
using System.Reflection;

namespace Samp.Core.Extensions
{
    public static class SwaggerExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(f =>
                        !f.IsDynamic
                        && !f.FullName.StartsWith("Microsoft.")
                        && !f.FullName.StartsWith("System.")
                        && !f.FullName.StartsWith("xunit.")
                        && !f.FullName.StartsWith("System,")
                        && !f.FullName.StartsWith("AutoMapper,")
                        && !f.FullName.Equals(typeof(BaseController).Assembly.FullName)
                        && f.DefinedTypes.Any(x => x.IsAssignableTo(typeof(BaseController)))
                     );

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

                var xmlFilePath = $"{assemblies.First().Location}.xml";
                xmlFilePath = xmlFilePath.Replace(".dll.xml", ".xml", StringComparison.InvariantCultureIgnoreCase);
                sgo.IncludeXmlComments(xmlFilePath);
            });
            return services;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            var asm = Assembly.GetCallingAssembly();
            app.UseSwagger(c => c.SerializeAsV2 = false);
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", asm.FullName.Split(',')[0]);
            });

            return app;
        }
    }
}