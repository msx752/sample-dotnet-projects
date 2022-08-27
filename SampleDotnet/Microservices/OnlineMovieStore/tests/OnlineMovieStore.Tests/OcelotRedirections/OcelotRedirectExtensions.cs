namespace SampleProject.Tests.OcelotRedirections;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ocelot.Requester;
using System;
using System.Net.Http;

public static class OcelotRedirectExtensions
{
    public static void AddOcelotWAFRedirections(this IServiceCollection services, HttpClient[] httpClients)
    {
        if (httpClients == null)
        {
            return;
        }

        var _ocelotMessageHandlers = new CustomOcelotMessageHandler(httpClients);

        services.AddSingleton(typeof(ICustomOcelotMessageHandler), x => _ocelotMessageHandlers);

        var descriptorIHttpRequester = new ServiceDescriptor(typeof(IHttpRequester), typeof(CustomHttpClientHttpRequester),
            ServiceLifetime.Singleton);

        services.Replace(descriptorIHttpRequester);
    }

    /// <summary>
    /// creates virtual HttpContext for the WebApplicationFactory for given url format.
    /// </summary>
    /// <typeparam name="TStartup"></typeparam>
    /// <param name="webApplicationFactory"></param>
    /// <param name="port">virtual port default: 80</param>
    /// <param name="host">virtual hostname default: localhost</param>
    /// <param name="scheme">virtual scheme default: http</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static HttpClient CreateOcelotClient<TStartup>(this WebApplicationFactory<TStartup> webApplicationFactory,
        int port,
        string host = "localhost")
    where TStartup : class
    {
        if (Uri.TryCreate(new Uri($"http://{host}:{port}"), "", out Uri generatedVirtualuri))
        {
            return webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions()
            {
                BaseAddress = generatedVirtualuri
            });
        }

        throw new ArgumentException($"Invalid Uri Format: '{$"http://{host}:{port}"}' please use valid Uri format.");
    }
}