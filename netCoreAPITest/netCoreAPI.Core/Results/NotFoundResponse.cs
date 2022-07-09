using Microsoft.AspNetCore.Http;
using netCoreAPI.Core.Results.Abstracts;

namespace netCoreAPI.Core.Results
{
    public sealed class NotFoundResponse
        : BaseResult
    {
        public NotFoundResponse()
            : base(StatusCodes.Status404NotFound)
        {
        }
    }
}