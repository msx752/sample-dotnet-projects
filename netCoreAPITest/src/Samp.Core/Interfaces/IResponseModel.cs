using System.Collections.Generic;

namespace Samp.Core.Interfaces
{
    public interface IResponseModel
    {
        public string ElapsedMilliseconds { get; set; }
        string RId { get; set; }
        List<string> Errors { get; set; }
    }

    public interface IResponseModel<T>
        : IResponseModel
        where T : class
    {
        public List<T> Result { get; set; }
    }
}