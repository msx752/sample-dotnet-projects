using Microsoft.AspNetCore.Http;
using Samp.Result.Abstractions;
using Samp.Result.Interfaces;

namespace Samp.Result.Executors
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