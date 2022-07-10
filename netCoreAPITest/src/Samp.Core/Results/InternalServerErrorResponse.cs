using Microsoft.AspNetCore.Http;
using Samp.Core.Results.Abstracts;

namespace Samp.Core.Results
{
    public sealed class InternalServerErrorResponse
        : BaseResult
    {
        public InternalServerErrorResponse()
            : base(StatusCodes.Status500InternalServerError)
        {
        }
    }
}