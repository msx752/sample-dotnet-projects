using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using netCoreAPI.Core.AppSettings;
using netCoreAPI.Core.Interfaces;
using netCoreAPI.Core.Interfaces.Repositories.Shared;
using System;
using System.Linq;

namespace netCoreAPI.Core.Extensions
{
    public static class StartupExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalStartupConfigures(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseContextSeed();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            return app;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TApplicationSettings"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="addScopedServices"></param>
        /// <returns></returns>
        public static IServiceCollection AddGlobalStartupServices<TApplicationSettings>(this IServiceCollection services, IConfiguration configuration, params Type[] addScopedServices)
            where TApplicationSettings : ApplicationSettings
        {
            services.Configure<TApplicationSettings>(configuration);
            services.AddEntityMapper();
            services.AddHttpContextAccessor();
            services.AddJWTAuthentication(configuration);
            services.AddSwagger();
            services.AddControllers().AddNewtonsoftJson();

            foreach (var item in addScopedServices)
            {
                if (typeof(IContextSeed).IsAssignableFrom(item))
                {
                    services.AddDbContextSeed(item);
                }
                else if (typeof(ISharedConnection<>).IsAssignableFrom(item))
                {
                    var iGenericSharedConnection = typeof(ISharedConnection<>)
                        .MakeGenericType(item.GenericTypeArguments[0]);

                    services.AddScoped(iGenericSharedConnection, item);
                }
                else if (typeof(ISharedRepository).IsAssignableFrom(item))
                {
                    services.AddScoped(typeof(ISharedRepository), item);
                }
                else
                {
                    var implementedInterfaceType = ((System.Reflection.TypeInfo)item).ImplementedInterfaces.FirstOrDefault();

                    if (implementedInterfaceType == null)
                    {
                        throw new Exception($"Invalid implementationType found for the '{item}'");
                    }

                    services.AddScoped(implementedInterfaceType, item);
                }
            }

            return services;
        }
    }
}