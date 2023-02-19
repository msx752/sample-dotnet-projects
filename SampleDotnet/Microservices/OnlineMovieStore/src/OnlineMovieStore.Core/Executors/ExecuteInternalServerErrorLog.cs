using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SampleProject.Result;
using SampleProject.Result.Abstractions;
using SampleProject.Result.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnlineMovieStore.Core.Executors
{
    public class ExecuteInternalServerErrorLog : IBaseResultExecutor
    {
        public Task OnBeforeActionResultExecutorAsync(HttpContext httpContext, IServiceProvider serviceProvider, BaseJsonResult jsonResult)
        {
            if (jsonResult.GetType() != typeof(InternalServerErrorResponse))
                return Task.CompletedTask;

            return Task.Run(() =>
            {
                Exception exception = httpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;
                if (exception != null)
                {
                    jsonResult.Model.Errors.Add(exception.ToString()); //debugging purpsose
                }
            });
        }
    }
}