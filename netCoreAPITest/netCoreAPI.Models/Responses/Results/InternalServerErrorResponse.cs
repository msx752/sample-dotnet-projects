using Microsoft.AspNetCore.Http;
using netCoreAPI.Models.Responses;

namespace netCoreAPI.Models.Responses.Results
{
    public class InternalServerErrorResponse
        : BaseResult
    {
        public InternalServerErrorResponse()
            : base(StatusCodes.Status500InternalServerError)
        {
        }
    }
}