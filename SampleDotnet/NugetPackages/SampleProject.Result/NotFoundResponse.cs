namespace SampleProject.Result;

public sealed class NotFoundResponse
    : BaseJsonResult
{
    public NotFoundResponse()
        : base(StatusCodes.Status404NotFound)
    {
    }

    public NotFoundResponse(string message)
        : base(StatusCodes.Status400BadRequest, message)
    {
    }
}