using Microsoft.AspNetCore.Http;

namespace netCoreAPI.Models.Responses.Results
{
    public class NotFoundResponse
        : BaseResult
    {
        public NotFoundResponse()
            : base(StatusCodes.Status404NotFound)
        {
        }
    }
}