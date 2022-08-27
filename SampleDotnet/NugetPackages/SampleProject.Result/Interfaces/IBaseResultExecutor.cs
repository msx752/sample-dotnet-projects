using Microsoft.AspNetCore.Http;
using SampleProject.Result.Abstractions;

namespace SampleProject.Result.Interfaces
{
    public interface IBaseResultExecutor
    {
        Task ExecuteAsync(HttpContext context, BaseResult baseResult);
    }
}