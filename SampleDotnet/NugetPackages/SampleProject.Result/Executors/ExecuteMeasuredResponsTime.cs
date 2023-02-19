namespace SampleProject.Result.Executors;

public class ExecuteMeasuredResponsTime : IBaseResultExecutor
{
    public Task OnBeforeActionResultExecutorAsync(HttpContext context, IServiceProvider serviceProvider, BaseJsonResult jsonResult)
    {
        return Task.Run(() =>
        {
            var requestStartDateTime = DateTimeOffset.Parse(context.Items[_Constants.RequestStartTime].ToString());
            var dtNow = DateTimeOffset.UtcNow;
            jsonResult.Model.Stats.Offset = dtNow.ToUnixTimeSeconds();
            var elapsedResponseTime = dtNow - requestStartDateTime;
            jsonResult.Model.Stats.ElapsedMilliseconds = elapsedResponseTime.TotalMilliseconds.ToString("####0.0", CultureInfo.InvariantCulture);
        });
    }
}