namespace SampleProject.Result;

public sealed class UnauthorizedResponse
    : BaseJsonResult
{
    public UnauthorizedResponse()
        : base(StatusCodes.Status401Unauthorized, "Unauthorized")
    {
    }

    public UnauthorizedResponse(string message)
        : base(StatusCodes.Status401Unauthorized, message)
    {
    }

    public UnauthorizedResponse(IEnumerable<string> messages)
        : base(StatusCodes.Status401Unauthorized, messages)
    {
    }
}