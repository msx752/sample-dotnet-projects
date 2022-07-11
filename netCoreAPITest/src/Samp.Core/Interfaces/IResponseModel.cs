using Samp.Core.Results.Abstracts;
using System.Collections.Generic;

namespace Samp.Core.Interfaces
{
    public interface IResponseModel
    {
        List<string> Errors { get; set; }
        ResponseReportModel Reports { get; set; }
    }

    public interface IResponseModel<T>
        : IResponseModel
        where T : class
    {
        public List<T> Result { get; set; }
    }
}