using Microsoft.AspNetCore.Http;
using Samp.Result.Abstractions;
using System.Collections.Generic;

namespace Samp.Result
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