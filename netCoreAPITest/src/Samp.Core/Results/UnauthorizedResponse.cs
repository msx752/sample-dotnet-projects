using Microsoft.AspNetCore.Http;
using Samp.Core.Results.Abstracts;
using System.Collections.Generic;

namespace Samp.Core.Results
{
    public sealed class UnauthorizedResponse
        : BaseResult
    {
        public UnauthorizedResponse()
            : base(StatusCodes.Status401Unauthorized)
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