using Microsoft.AspNetCore.Http;
using Samp.Result.Abstractions;

namespace Samp.Result
{
    public sealed class NotFoundResponse
        : BaseResult
    {
        public NotFoundResponse()
            : base(StatusCodes.Status404NotFound)
        {
        }

        public NotFoundResponse(string message)
            : base(StatusCodes.Status400BadRequest, message)
        {
        }
    }
}