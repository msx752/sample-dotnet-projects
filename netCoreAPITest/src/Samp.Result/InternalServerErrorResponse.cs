using Microsoft.AspNetCore.Http;
using Samp.Result.Abstractions;
using System.Collections.Generic;

namespace Samp.Result
{
    public sealed class InternalServerErrorResponse
        : BaseResult
    {
        public InternalServerErrorResponse()
            : base(StatusCodes.Status500InternalServerError)
        {
        }

        public InternalServerErrorResponse(string userFriendlyMessage)
            : base(StatusCodes.Status500InternalServerError, userFriendlyMessage)
        {
        }

        public InternalServerErrorResponse(IEnumerable<string> userFriendlyMessages)
            : base(StatusCodes.Status500InternalServerError, userFriendlyMessages)
        {
        }
    }
}