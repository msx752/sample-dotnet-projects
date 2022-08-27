﻿using Microsoft.AspNetCore.Http;
using SampleProject.Result.Abstractions;
using System.Collections.Generic;

namespace SampleProject.Result
{
    public sealed class BadRequestResponse
        : BaseResult
    {
        public BadRequestResponse()
            : base(StatusCodes.Status400BadRequest)
        {
        }

        public BadRequestResponse(string message)
            : base(StatusCodes.Status400BadRequest, message)
        {
        }

        public BadRequestResponse(IEnumerable<string> messages)
            : base(StatusCodes.Status400BadRequest, messages)
        {
        }
    }
}