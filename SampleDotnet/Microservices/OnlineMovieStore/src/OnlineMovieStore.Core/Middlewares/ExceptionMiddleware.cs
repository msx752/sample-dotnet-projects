using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using SampleProject.Result;
using System.Collections.Generic;

namespace SampleProject.Core.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(c => c.Run(async httpContext =>
            {
                var exception = httpContext.Features.Get<IExceptionHandlerPathFeature>().Error;

                var routeData = httpContext.GetRouteData();
                var actionContext = new ActionContext(httpContext, routeData, new ActionDescriptor());

                List<string> messages = new List<string>() { exception.Message };

                messages.Add(exception.ToString());

                var result = new InternalServerErrorResponse(messages);
                await result.ExecuteResultAsync(actionContext);
            }));
        }
    }
}