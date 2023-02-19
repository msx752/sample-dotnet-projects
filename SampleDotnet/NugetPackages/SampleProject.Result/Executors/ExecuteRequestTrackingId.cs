using Microsoft.AspNetCore.Http;
using SampleProject.Result.Abstractions;
using SampleProject.Result.Interfaces;

namespace SampleProject.Result.Executors
{
    public class ExecuteRequestTrackingId : IBaseResultExecutor
    {
        public Task ExecuteAsync(HttpContext context, BaseJsonResult jsonResult)
        {
            return Task.Run(() =>
            {
                jsonResult.Model.Stats.RId = System.Diagnostics.Activity.Current?.RootId;
            });
        }
    }
}