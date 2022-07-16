using Microsoft.AspNetCore.Http;
using Samp.Core.Results.Abstracts;
using System.Collections.Generic;

namespace Samp.Core.Results
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