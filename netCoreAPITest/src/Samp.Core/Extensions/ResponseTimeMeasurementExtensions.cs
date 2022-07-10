using Microsoft.AspNetCore.Builder;
using Samp.Core.Results.Abstracts;
using System;

namespace Samp.Core.Extensions
{
    public static class ResponseTimeMeasurementExtensions
    {
        /// <summary>
        /// sets RequestStartTime for the <see cref="BaseResult"/>'s ExecuteResultAsync method
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseElapsedTimeMeasurement(this IApplicationBuilder application)
        {
            application.Use(async (context, next) =>
            {
                context.Items.Add(Constants.RequestStartTime, DateTime.UtcNow.ToString());
                await next();
            });
            return application;
        }
    }
}