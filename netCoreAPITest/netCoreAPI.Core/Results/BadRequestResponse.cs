using Microsoft.AspNetCore.Http;
using netCoreAPI.Core.Results.Abstracts;
using System.Collections.Generic;

namespace netCoreAPI.Core.Results
{
    public sealed class BadRequestResponse
        : BaseResult
    {
        public BadRequestResponse()
            : base(StatusCodes.Status400BadRequest)
        {
        }

        public BadRequestResponse(string message)
            : base(StatusCodes.Status400BadRequest, message)
        {
        }

        public BadRequestResponse(IEnumerable<string> messages)
            : base(StatusCodes.Status400BadRequest, messages)
        {
        }
    }
}