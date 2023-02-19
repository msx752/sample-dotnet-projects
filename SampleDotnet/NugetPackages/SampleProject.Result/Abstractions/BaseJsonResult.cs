namespace SampleProject.Result.Abstractions;

public class BaseJsonResult : JsonResult
{
    public BaseJsonResult(int statusCode)
        : base(new ResponseModel())

    {
        ContentType = "application/json";
        StatusCode = statusCode;
    }

    public BaseJsonResult(int statusCode, IEnumerable<string> errorMessages)
        : base(new ResponseModel(errorMessages))
    {
        ContentType = "application/json";
        StatusCode = statusCode;
    }

    public BaseJsonResult(int statusCode, string errorMessages)
        : base(new ResponseModel(errorMessages))
    {
        ContentType = "application/json";
        StatusCode = statusCode;
    }

    public BaseJsonResult(int statusCode, object body)
        : base(new ResponseModel<object>(body))
    {
        ContentType = "application/json";
        StatusCode = statusCode;

        SerializerSettings = DefaultJsonSerializerSettings();
    }

    public BaseJsonResult(int statusCode, IEnumerable<object> body)
        : base(new ResponseModel<object>(body), null)
    {
        ContentType = "application/json";
        StatusCode = statusCode;

        SerializerSettings = DefaultJsonSerializerSettings();
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
        ArgumentNullException.ThrowIfNull(StatusCode, nameof(StatusCode));

        var httpContext = context.HttpContext;
        httpContext.Response.StatusCode = StatusCode.Value;

        var serviceProvider = httpContext.RequestServices;
        var resultExecutors = serviceProvider.GetServices<IBaseResultExecutor>();

        foreach (var execute in resultExecutors)
            await execute.OnBeforeActionResultExecutorAsync(httpContext, serviceProvider, this);

        var executor = serviceProvider.GetService<IActionResultExecutor<JsonResult>>();
        ArgumentNullException.ThrowIfNull(executor, nameof(IActionResultExecutor<JsonResult>));

        await executor.ExecuteAsync(context, this);
    }

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    public IResponseModel Model
    {
        get
        {
            ArgumentNullException.ThrowIfNull(Value, nameof(Value));
            return (IResponseModel)Value;
        }
    }
}