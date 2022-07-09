using Microsoft.AspNetCore.Http;

namespace netCoreAPI.Models.Responses.Results
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