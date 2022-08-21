using Microsoft.AspNetCore.Http;
using Samp.Result.Abstractions;
using System.Collections.Generic;

namespace Samp.Result
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