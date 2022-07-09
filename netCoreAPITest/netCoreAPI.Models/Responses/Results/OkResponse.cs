using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace netCoreAPI.Models.Responses.Results
{
    public class OkResponse
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