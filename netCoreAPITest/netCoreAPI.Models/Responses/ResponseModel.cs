using netCoreAPI.Models.Interfaces;
using System.Collections.Generic;

namespace netCoreAPI.Models.Responses
{
    public sealed class ResponseModel<T>
        : IBaseResponseModel<T>
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
        {
            Errors = new();
            Errors.AddRange(messages);
        }

        public ResponseModel(string message)
        {
            Errors = new();
            Errors.Add(message);
        }

        public List<string> Errors { get; set; }
        public List<T> Result { get; set; }
    }

    public sealed class ResponseModel
        : IBaseResponseModel
    {
        public ResponseModel()
        {
        }

        public ResponseModel(IEnumerable<string> messages)
            : base()
        {
            Errors = new();
            Errors.AddRange(messages);
        }

        public ResponseModel(string message)
            : base()
        {
            Errors = new();
            Errors.Add(message);
        }

        public List<string> Errors { get; set; }
    }
}