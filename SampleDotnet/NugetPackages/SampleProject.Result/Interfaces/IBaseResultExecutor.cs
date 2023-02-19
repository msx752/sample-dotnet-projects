using Microsoft.AspNetCore.Http;
using SampleProject.Result.Abstractions;

namespace SampleProject.Result.Interfaces
{
    public interface IBaseResultExecutor
    {
        Task OnBeforeActionResultExecutorAsync(HttpContext context, IServiceProvider serviceProvider, BaseJsonResult baseResult);
    }
}