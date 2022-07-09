using System.Collections.Generic;

namespace netCoreAPI.Models.Interfaces
{
    public interface IResponseModel
    {
        List<string> Errors { get; set; }
    }

    public interface IResponseModel<T>
        : IResponseModel
        where T : class
    {
        public List<T> Result { get; set; }
    }
}