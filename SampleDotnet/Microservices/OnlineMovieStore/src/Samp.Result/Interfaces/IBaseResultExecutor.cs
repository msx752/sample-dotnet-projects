using Microsoft.AspNetCore.Http;
using Samp.Result.Abstractions;

namespace Samp.Result.Interfaces
{
    public interface IBaseResultExecutor
    {
        Task ExecuteAsync(HttpContext context, BaseResult baseResult);
    }
}