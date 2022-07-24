using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Samp.Core.AppSettings;
using Samp.Core.Middlewares;

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
            app.UseExceptionHandlerMiddleware();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
            }

            app.UseElapsedTimeMeasurement();
            app.UseContextSeed();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

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
        public static IServiceCollection AddGlobalStartupServices<TApplicationSettings>(this IServiceCollection services, IConfiguration configuration)
        where TApplicationSettings : ApplicationSettings
        {
            services.Configure<TApplicationSettings>(configuration);
            services.AddEntityMapper();
            services.AddHttpContextAccessor();
            services.AddJWTAuthentication(configuration);
            services.AddSwagger();
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();

            return services;
        }
    }
}