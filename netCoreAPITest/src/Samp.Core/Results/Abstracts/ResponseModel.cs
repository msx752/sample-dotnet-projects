using Samp.Core.Interfaces;
using System.Collections.Generic;

namespace Samp.Core.Results.Abstracts
{
    public abstract class BaseResponseModel
    {
        public BaseResponseModel()
        {
            Reports = new();
        }

        public BaseResponseModel(IEnumerable<string> messages)
        {
            Errors = new();
            Errors.AddRange(messages);
        }

        public BaseResponseModel(string message)
        {
            Errors = new();
            Errors.Add(message);
        }

        public List<string> Errors { get; set; }
        public ResponseReportModel Reports { get; set; }
    }

    public class ResponseReportModel
    {
        public string RId { get; set; }
        public string ElapsedMilliseconds { get; set; }
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
            Result = new();
            Result.AddRange(body);
        }

        public ResponseModel(T body)
        {
            Result = new();
            Result.Add(body);
        }

        public ResponseModel(IEnumerable<string> messages)
            : base(messages)
        {
        }

        public ResponseModel(string message)
            : base(message)
        {
        }

        public List<T> Result { get; set; }
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