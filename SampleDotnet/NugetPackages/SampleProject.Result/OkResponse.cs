using Microsoft.AspNetCore.Http;
using SampleProject.Result.Abstractions;

namespace SampleProject.Result
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