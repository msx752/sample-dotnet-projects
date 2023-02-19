namespace SampleProject.Result.Abstractions;

public class BaseJsonResult : JsonResult
{
    public BaseJsonResult(int statusCode)
        : base(new ResponseModel())

    {
        StatusCode = statusCode;
        ContentType = _Constants.ContentType_ApplicationJson;
        SerializerSettings = ConfigureJsonSerializerSettings();
    }

    public BaseJsonResult(int statusCode, IEnumerable<string> errorMessages)
        : base(new ResponseModel(errorMessages))
    {
        StatusCode = statusCode;
        ContentType = _Constants.ContentType_ApplicationJson;
        SerializerSettings = ConfigureJsonSerializerSettings();
    }

    public BaseJsonResult(int statusCode, string errorMessages)
        : base(new ResponseModel(errorMessages))
    {
        StatusCode = statusCode;
        ContentType = _Constants.ContentType_ApplicationJson;
        SerializerSettings = ConfigureJsonSerializerSettings();
    }

    public BaseJsonResult(int statusCode, object body)
        : base(new ResponseModel<object>(body))
    {
        StatusCode = statusCode;
        ContentType = _Constants.ContentType_ApplicationJson;
        SerializerSettings = ConfigureJsonSerializerSettings();
    }

    public BaseJsonResult(int statusCode, IEnumerable<object> body)
        : base(new ResponseModel<object>(body), null)
    {
        StatusCode = statusCode;
        ContentType = _Constants.ContentType_ApplicationJson;
        SerializerSettings = ConfigureJsonSerializerSettings();
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

    public virtual JsonSerializerSettings ConfigureJsonSerializerSettings()
    {
        return JsonConvert.DefaultSettings != null ? JsonConvert.DefaultSettings() : new JsonSerializerSettings();
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

        if (this.Model.Stats is ExpandoObject eobj && !eobj.Any())
            this.Model.Stats = null;

        await executor.ExecuteAsync(context, this);
    }
}