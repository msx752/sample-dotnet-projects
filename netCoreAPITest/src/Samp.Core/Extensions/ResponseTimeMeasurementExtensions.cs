using Microsoft.AspNetCore.Builder;
using System;

namespace Samp.Core.Extensions
{
    public static class ResponseTimeMeasurementExtensions
    {
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