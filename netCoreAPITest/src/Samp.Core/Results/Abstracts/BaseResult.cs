using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Samp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Samp.Core.Results.Abstracts
{
    public abstract partial class BaseResult : JsonResult
    {
        public BaseResult(int statusCode)
            : base(new ResponseModel())

        {
            StatusCode = statusCode;
        }

        public BaseResult(int statusCode, IEnumerable<string> messages)
            : base(new ResponseModel(messages))
        {
            StatusCode = statusCode;
        }

        public BaseResult(int statusCode, string message)
            : base(new ResponseModel(message))
        {
            StatusCode = statusCode;
        }

        public BaseResult(int statusCode, object body)
            : base(new ResponseModel<object>(body))
        {
            StatusCode = statusCode;

            SerializerSettings = DefaultJsonSerializerSettings();
        }

        public BaseResult(int statusCode, IEnumerable<object> body)
            : base(new ResponseModel<object>(body), null)
        {
            StatusCode = statusCode;

            SerializerSettings = DefaultJsonSerializerSettings();
        }

        protected BaseResult(object body, JsonSerializerSettings serializerSettings)
            : base(new ResponseModel<object>(body), serializerSettings)
        {
        }

        public virtual JsonSerializerSettings DefaultJsonSerializerSettings()
        {
            return new JsonSerializerSettings()
            {
                //NullValueHandling = NullValueHandling.Include,
                //Formatting = Formatting.None,
                //Culture = new System.Globalization.CultureInfo("en-us"),
                //DateParseHandling = DateParseHandling.DateTimeOffset,
                //ContractResolver = new DefaultContractResolver()
            };
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var httpContext = context.HttpContext;
            var services = httpContext.RequestServices;
            var factory = services.GetRequiredService<ILoggerFactory>();
            var logger = factory.CreateLogger(this.GetType());

            Log.HttpStatusCodeResultExecuting(logger, StatusCode.Value);

            httpContext.Response.StatusCode = StatusCode.Value;

            ((IResponseModel)Value).RId = System.Diagnostics.Activity.Current?.RootId;

            var executor = services.GetRequiredService<IActionResultExecutor<JsonResult>>();
            return executor.ExecuteAsync(context, this);
        }

        private static partial class Log
        {
            [LoggerMessage(1, LogLevel.Information, "Executing StatusCodeResult, setting HTTP status code {StatusCode}", EventName = "HttpStatusCodeResultExecuting")]
            public static partial void HttpStatusCodeResultExecuting(ILogger logger, int statusCode);
        }
    }
}