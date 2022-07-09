using System.Collections.Generic;

namespace netCoreAPI.Core.Interfaces
{
    public interface IResponseModel
    {
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