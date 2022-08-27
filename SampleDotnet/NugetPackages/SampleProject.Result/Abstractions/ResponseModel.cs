using Newtonsoft.Json;
using SampleProject.Result.Interfaces;

namespace SampleProject.Result.Abstractions
{
    public abstract class BaseResponseModel
    {
        public BaseResponseModel()
        {
            Stats = new();
        }

        public BaseResponseModel(IEnumerable<string> messages)
            : this()
        {
            Errors = new();
            Errors.AddRange(messages);
        }

        public BaseResponseModel(string message)
            : this()
        {
            Errors = new();
            Errors.Add(message);
        }

        [JsonProperty("errors")]
        public List<string> Errors { get; set; }

        [JsonProperty("stats")]
        public ResponseStatModel Stats { get; set; }
    }

    public class ResponseStatModel
    {
        [JsonProperty("rid")]
        public string RId { get; set; }

        [JsonProperty("elapsedmilliseconds")]
        public string ElapsedMilliseconds { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }
    }

    public sealed class ResponseModel<T>
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

        public ResponseModel(IEnumerable<string> messages)
            : base(messages)
        {
        }

        public ResponseModel(string message)
            : base(message)
        {
        }

        [JsonProperty("results")]
        public List<T> Results { get; set; }
    }

    public sealed class ResponseModel
        : BaseResponseModel
        , IResponseModel
    {
        public ResponseModel()
            : base()
        {
        }

        public ResponseModel(IEnumerable<string> messages)
            : base(messages)
        {
        }

        public ResponseModel(string message)
            : base(message)
        {
        }
    }
}