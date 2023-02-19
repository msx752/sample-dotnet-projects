namespace SampleProject.Result;

public sealed class InternalServerErrorResponse
    : BaseJsonResult
{
    public InternalServerErrorResponse()
        : base(StatusCodes.Status500InternalServerError)
    {
    }

    public InternalServerErrorResponse(string userFriendlyMessage)
        : base(StatusCodes.Status500InternalServerError, userFriendlyMessage)
    {
    }

    public InternalServerErrorResponse(IEnumerable<string> userFriendlyMessages)
        : base(StatusCodes.Status500InternalServerError, userFriendlyMessages)
    {
    }
}