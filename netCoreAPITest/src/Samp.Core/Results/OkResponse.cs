using Microsoft.AspNetCore.Http;
using Samp.Core.Results.Abstracts;
using System.Collections.Generic;

namespace Samp.Core.Results
{
    public sealed class OkResponse
        : BaseResult
    {
        public OkResponse()
            : base(StatusCodes.Status200OK)
        {
        }

        public OkResponse(IEnumerable<object> body)
            : base(StatusCodes.Status200OK, body)
        {
        }

        public OkResponse(object body)
            : base(StatusCodes.Status200OK, body)
        {
        }
    }
}