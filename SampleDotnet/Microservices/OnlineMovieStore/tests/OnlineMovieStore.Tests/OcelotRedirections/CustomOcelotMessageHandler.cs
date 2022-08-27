namespace SampleProject.Tests.OcelotRedirections;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using Ocelot.Configuration;

public class CustomOcelotMessageHandler : ICustomOcelotMessageHandler
{
    private readonly Dictionary<string, HttpMessageHandler> handlers = new Dictionary<string, HttpMessageHandler>();

    public CustomOcelotMessageHandler(HttpClient[] httpClients)
    {
        foreach (var item in httpClients)
        {
            var kv = GetHttpClientHandler(item);
            handlers.Add(kv.Key, kv.Value);
        }
    }

    public HttpMessageHandler Gethandler(DownstreamRoute downstreamRoute)
    {
        foreach (var downstreamHostAndPort in downstreamRoute.DownstreamAddresses)
        {
            if (handlers.TryGetValue($"{downstreamHostAndPort.Host.ToLowerInvariant()}:{downstreamHostAndPort.Port}", out HttpMessageHandler handler))
            {
                return handler;
            }
        }

        return null;
    }

    private static KeyValuePair<string, HttpMessageHandler> GetHttpClientHandler(HttpClient httpClient)
    {
        var handlerField = httpClient
            .GetType().BaseType
            .GetField("_handler", BindingFlags.NonPublic | BindingFlags.Instance);

        var handler = (HttpMessageHandler)handlerField.GetValue(httpClient);
        var handlerType = handler.GetType();

        if (!handlerType.FullName.StartsWith("Microsoft.AspNetCore.Mvc.Testing"))
        {
            throw new ArgumentException($"selected HttpClient doesn't belongs to WebApplicationFactory, this is only for Testing purpose.");
        }

        return new KeyValuePair<string, HttpMessageHandler>($"{httpClient.BaseAddress.Host.ToLowerInvariant()}:{httpClient.BaseAddress.Port}", handler);
    }
}