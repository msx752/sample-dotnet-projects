using Microsoft.AspNetCore.Builder;
using SampleProject.Result.Abstractions;

namespace SampleProject.Result.Extensions
{
    public static class ResponseTimeMeasurementExtensions
    {
        /// <summary>
        /// sets RequestStartTime for the <see cref="BaseJsonResult"/>'s ExecuteResultAsync method
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseElapsedTimeMeasurement(this IApplicationBuilder application)
        {
            application.Use(async (context, next) =>
            {
                context.Items.Add(_Constants.RequestStartTime, DateTimeOffset.UtcNow.ToString());
                await next();
            });
            return application;
        }
    }
}