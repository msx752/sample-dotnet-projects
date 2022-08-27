using Microsoft.AspNetCore.Http;
using SampleProject.Result.Abstractions;

namespace SampleProject.Result
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