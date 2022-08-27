using Microsoft.AspNetCore.Http;
using SampleProject.Result.Abstractions;
using System.Collections.Generic;

namespace SampleProject.Result
{
    public sealed class UnauthorizedResponse
        : BaseResult
    {
        public UnauthorizedResponse()
            : base(StatusCodes.Status401Unauthorized, "Unauthorized")
        {
        }

        public UnauthorizedResponse(string message)
            : base(StatusCodes.Status401Unauthorized, message)
        {
        }

        public UnauthorizedResponse(IEnumerable<string> messages)
            : base(StatusCodes.Status401Unauthorized, messages)
        {
        }
    }
}