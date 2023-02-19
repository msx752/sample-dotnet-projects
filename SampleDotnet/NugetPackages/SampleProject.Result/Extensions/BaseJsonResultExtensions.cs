namespace SampleProject.Result.Extensions;

internal static class BaseJsonResultExtensions
{
    internal static void DefaultCtorValues(this BaseJsonResult baseJsonResult)
    {
        baseJsonResult.ContentType = _Constants.ContentType_ApplicationJson;
        baseJsonResult.SerializerSettings = baseJsonResult.ConfigureJsonSerializerSettings();
    }
}