namespace SampleProject.Result.Executors;

public class ExecuteRequestTrackingId : IBaseResultExecutor
{
    public Task OnBeforeActionResultExecutorAsync(HttpContext context, IServiceProvider serviceProvider, BaseJsonResult jsonResult)
    {
        return Task.Run(() =>
        {
            jsonResult.Model.Stats.rid = System.Diagnostics.Activity.Current?.RootId;
        });
    }
}