using System.Dynamic;

namespace SampleProject.Result.Abstractions;

public class BaseResponseModel
{
    public BaseResponseModel()
    {
        Stats = new ExpandoObject();
    }

    public BaseResponseModel(IEnumerable<string> errorMessages)
        : this()
    {
        Errors = new();
        Errors.AddRange(errorMessages);
    }

    public BaseResponseModel(string errorMessages)
        : this()
    {
        Errors = new();
        Errors.Add(errorMessages);
    }

    [JsonProperty("errors", NullValueHandling = NullValueHandling.Include)]
    public List<string> Errors { get; set; }

    [JsonProperty("stats", NullValueHandling = NullValueHandling.Ignore)]
    public dynamic Stats { get; set; }
}

//public class ResponseStatModel
//{
//    [JsonProperty("rid")]
//    public string RId { get; set; }

//    [JsonProperty("elapsedmilliseconds")]
//    public string ElapsedMilliseconds { get; set; }

//    [JsonProperty("offset")]
//    public long Offset { get; set; }
//}

public class ResponseModel<T>
        : BaseResponseModel
    , IResponseModel<T>
    where T : class
{
    public ResponseModel()
    {
    }

    public ResponseModel(IEnumerable<T> body)
    {
        Results = new();
        Results.AddRange(body);
    }

    public ResponseModel(T body)
    {
        Results = new();
        Results.Add(body);
    }

    public ResponseModel(IEnumerable<string> errorMessages)
        : base(errorMessages)
    {
    }

    public ResponseModel(string errorMessages)
        : base(errorMessages)
    {
    }

    [JsonProperty("results", NullValueHandling = NullValueHandling.Include)]
    public List<T> Results { get; set; }
}

public class ResponseModel
    : BaseResponseModel
    , IResponseModel
{
    public ResponseModel()
        : base()
    {
    }

    public ResponseModel(IEnumerable<string> errorMessages)
        : base(errorMessages)
    {
    }

    public ResponseModel(string errorMessages)
        : base(errorMessages)
    {
    }
}