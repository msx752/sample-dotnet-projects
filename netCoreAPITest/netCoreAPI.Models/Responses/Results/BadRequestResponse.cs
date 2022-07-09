using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace netCoreAPI.Models.Responses.Results
{
    public sealed class BadRequestResponse
        : BaseResult
    {
        public BadRequestResponse() : base(StatusCodes.Status400BadRequest)
        {
        }

        public BadRequestResponse(string message) : base(StatusCodes.Status400BadRequest, message)
        {
        }

        public BadRequestResponse(IEnumerable<string> messages) : base(StatusCodes.Status400BadRequest, messages)
        {
        }
    }
}