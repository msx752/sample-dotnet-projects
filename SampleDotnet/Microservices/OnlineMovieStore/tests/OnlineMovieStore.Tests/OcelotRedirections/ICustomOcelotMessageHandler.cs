namespace SampleProject.Tests.OcelotRedirections;

using System.Net.Http;
using Ocelot.Configuration;

public interface ICustomOcelotMessageHandler
{
    HttpMessageHandler Gethandler(DownstreamRoute downstreamRoute);
}