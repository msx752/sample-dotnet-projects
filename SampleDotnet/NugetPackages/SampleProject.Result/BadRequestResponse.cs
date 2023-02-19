namespace SampleProject.Result;

public sealed class BadRequestResponse
    : BaseJsonResult
{
    public BadRequestResponse()
        : base(StatusCodes.Status400BadRequest)
    {
    }

    public BadRequestResponse(string message)
        : base(StatusCodes.Status400BadRequest, message)
    {
    }

    public BadRequestResponse(IEnumerable<string> messages)
        : base(StatusCodes.Status400BadRequest, messages)
    {
    }
}