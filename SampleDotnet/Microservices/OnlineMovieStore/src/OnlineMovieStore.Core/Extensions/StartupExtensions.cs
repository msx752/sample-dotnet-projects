using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineMovieStore.Core;
using OnlineMovieStore.Core.Executors;
using SampleProject.Authentication.Extensions;
using SampleProject.Authentication.Middlewares;
using SampleProject.Core.AppSettings;
using SampleProject.Core.Middlewares;
using SampleProject.Result;
using SampleProject.Result.Executors;
using SampleProject.Result.Extensions;
using SampleProject.Result.Interfaces;

namespace SampleProject.Core.Extensions
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
                app.UseCustomSwagger();
            }

            app.UseElapsedTimeMeasurement();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<MyJwtMiddleware>();
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
            services.AddSingleton<IBaseResultExecutor, ExecuteRequestTrackingId>();
            services.AddSingleton<IBaseResultExecutor, ExecuteMeasuredResponsTime>();
            services.AddSingleton<IBaseResultExecutor, ExecuteInternalServerErrorLog>();

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.Configure<TApplicationSettings>(configuration);

            services.AddEntityMapper();
            services.AddHttpContextAccessor();
            services.AddJWTAuthentication(configuration, new UnauthorizedResponse());
            services.AddCustomSwagger();
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();

            return services;
        }
    }
}