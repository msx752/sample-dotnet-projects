using Microsoft.AspNetCore.Http;
using SampleProject.Result.Abstractions;
using SampleProject.Result.Interfaces;

namespace SampleProject.Result.Executors
{
    public class ExecuteRequestTrackingId : IBaseResultExecutor
    {
        public Task ExecuteAsync(HttpContext context, BaseResult baseResult)
        {
            return Task.Run(() =>
            {
                baseResult.Model.Stats.RId = System.Diagnostics.Activity.Current?.RootId;
            });
        }
    }
}