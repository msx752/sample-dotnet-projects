using System.Dynamic;

namespace SampleProject.Result.Interfaces;

public interface IResponseModel
{
    List<string> Errors { get; set; }
    dynamic Stats { get; set; }
}

public interface IResponseModel<T>
    : IResponseModel
    where T : class
{
    public List<T> Results { get; set; }
}