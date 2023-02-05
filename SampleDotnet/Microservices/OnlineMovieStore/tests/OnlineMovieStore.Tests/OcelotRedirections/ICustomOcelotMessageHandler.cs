namespace OnlineMovieStore.Tests.OcelotRedirections;

using Ocelot.Configuration;
using System.Net.Http;

public interface ICustomOcelotMessageHandler
{
    HttpMessageHandler Gethandler(DownstreamRoute downstreamRoute);
}