using Microsoft.AspNetCore.Http;
using netCoreAPI.Core.Results.Abstracts;
using System.Collections.Generic;

namespace netCoreAPI.Core.Results
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