using System.Collections.Generic;

namespace netCoreAPI.Models.Interfaces
{
    public interface IBaseResponseModel
    {
        List<string> Errors { get; set; }
    }

    public interface IBaseResponseModel<T>
        where T : class
    {
        public List<T> Result { get; set; }
        List<string> Errors { get; set; }
    }
}