using Microsoft.AspNetCore.Http;
using netCoreAPI.Core.Results.Abstracts;

namespace netCoreAPI.Core.Results
{
    public sealed class InternalServerErrorResponse
        : BaseResult
    {
        public InternalServerErrorResponse()
            : base(StatusCodes.Status500InternalServerError)
        {
        }
    }
}