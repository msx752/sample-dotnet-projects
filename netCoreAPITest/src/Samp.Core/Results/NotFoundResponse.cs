using Microsoft.AspNetCore.Http;
using Samp.Core.Results.Abstracts;

namespace Samp.Core.Results
{
    public sealed class NotFoundResponse
        : BaseResult
    {
        public NotFoundResponse()
            : base(StatusCodes.Status404NotFound)
        {
        }
    }
}