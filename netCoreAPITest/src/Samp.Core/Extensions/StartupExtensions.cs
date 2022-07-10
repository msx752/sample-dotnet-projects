using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using netCoreAPI.Core.AppSettings;
using Samp.Core.Interfaces;
using Samp.Core.Interfaces.Repositories.Shared;
using Samp.Core.RepositoryServices;
using System;

namespace Samp.Core.Extensions
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
        public static IServiceCollection AddGlobalStartupServices<TApplicationSettings>(this IServiceCollection services, IConfiguration configuration, Type[] dbcontexts, Type[] dbcontextseeds)
            where TApplicationSettings : ApplicationSettings
        {
            services.Configure<TApplicationSettings>(configuration);
            services.AddEntityMapper();
            services.AddHttpContextAccessor();
            services.AddJWTAuthentication(configuration);
            services.AddSwagger();
            services.AddControllers().AddNewtonsoftJson();

            if (dbcontexts != null)
            {
                foreach (var item in dbcontexts)
                {
                    if (typeof(DbContext).IsAssignableFrom(item))
                    {
                        var genericSharedConnection = typeof(SharedRepository<>)
                            .MakeGenericType(item);

                        var iGenericSharedConnection = typeof(ISharedRepository<>)
                            .MakeGenericType(item);

                        services.AddScoped(iGenericSharedConnection, genericSharedConnection);
                    }
                    else
                    {
                        throw new NotSupportedException($"invalid service type detected, expected:{nameof(ISharedRepository)}, found:{item}");
                    }
                }
            }

            if (dbcontextseeds != null)
            {
                foreach (var item in dbcontextseeds)
                {
                    if (typeof(IContextSeed).IsAssignableFrom(item))
                    {
                        services.AddDbContextSeed(item);
                    }
                    else
                    {
                        throw new NotSupportedException($"invalid service type detected, expected:{nameof(IContextSeed)}, found:{item}");
                    }
                }
            }

            return services;
        }
    }
}