using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SampleProject.Result.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Result.Abstractions
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
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
                Culture = new System.Globalization.CultureInfo("en-us"),
                DateParseHandling = DateParseHandling.DateTimeOffset,
                ContractResolver = new DefaultContractResolver(),
            };
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));

            var httpContext = context.HttpContext;
            httpContext.Response.StatusCode = StatusCode.Value;

            var services = httpContext.RequestServices;
            IEnumerable<IBaseResultExecutor> resultExecutors = services.GetServices<IBaseResultExecutor>();

            foreach (var execute in resultExecutors)
                await execute.ExecuteAsync(httpContext, this);

            var executor = services.GetRequiredService<IActionResultExecutor<JsonResult>>();
            await executor.ExecuteAsync(context, this);
        }

        [NotMapped]
        [JsonIgnore]
        internal IResponseModel Model => (IResponseModel)Value;
    }
}