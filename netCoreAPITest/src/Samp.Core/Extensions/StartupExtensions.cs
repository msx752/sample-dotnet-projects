using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            app.UseElapsedTimeMeasurement();
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
        public static IServiceCollection AddGlobalStartupServices<TApplicationSettings>(this IServiceCollection services, IConfiguration configuration, params IDbContextParameter[] dbContextParameters)
        where TApplicationSettings : ApplicationSettings
        {
            services.Configure<TApplicationSettings>(configuration);
            services.AddEntityMapper();
            services.AddHttpContextAccessor();
            services.AddJWTAuthentication(configuration);
            services.AddSwagger();
            services.AddControllers().AddNewtonsoftJson();

            foreach (var dbContextParameter in dbContextParameters)
            {
                ArgumentNullException.ThrowIfNull(dbContextParameter.DbContext, nameof(dbContextParameter.DbContext));

                #region DbContext Initializer

                services.AddCustomDbContext(dbContextParameter.DbContext, dbContextParameter.ActionDbContextOptionsBuilder);

                #endregion DbContext Initializer

                #region DbContext Repository Initializer

                var genericSharedRepository = typeof(SharedRepository<>).MakeGenericType(dbContextParameter.DbContext);
                var iGenericSharedRepository = typeof(ISharedRepository<>).MakeGenericType(dbContextParameter.DbContext);

                services.AddScoped(iGenericSharedRepository, genericSharedRepository);

                #endregion DbContext Repository Initializer

                #region DbContext Seed Initializer

                services.AddDbContextSeed(dbContextParameter.ContextSeed);

                #endregion DbContext Seed Initializer
            }

            return services;
        }
    }
}