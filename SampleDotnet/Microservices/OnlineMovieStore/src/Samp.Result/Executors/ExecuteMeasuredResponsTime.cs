using Microsoft.AspNetCore.Http;
using Samp.Result.Abstractions;
using Samp.Result.Interfaces;
using System.Globalization;

namespace Samp.Result.Executors
{
    public class ExecuteMeasuredResponsTime : IBaseResultExecutor
    {
        public Task ExecuteAsync(HttpContext context, BaseResult baseResult)
        {
            return Task.Run(() =>
            {
                var requestStartDateTime = DateTimeOffset.Parse(context.Items[_Constants.RequestStartTime].ToString());
                var dtNow = DateTimeOffset.UtcNow;
                baseResult.Model.Stats.Offset = dtNow.ToUnixTimeSeconds();
                var elapsedResponseTime = dtNow - requestStartDateTime;
                baseResult.Model.Stats.ElapsedMilliseconds = elapsedResponseTime.TotalMilliseconds.ToString("####0.0", CultureInfo.InvariantCulture);
            });
        }
    }
}