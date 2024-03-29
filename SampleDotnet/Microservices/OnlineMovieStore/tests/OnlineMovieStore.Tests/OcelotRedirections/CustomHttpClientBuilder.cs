namespace OnlineMovieStore.Tests.OcelotRedirections;

using Ocelot.Configuration;
using Ocelot.Logging;
using Ocelot.Requester;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

public class CustomHttpClientBuilder : IHttpClientBuilder
{
    private readonly IHttpClientCache _cacheHandlers;
    private readonly ICustomOcelotMessageHandler _customOcelotMessageHandler;
    private readonly TimeSpan _defaultTimeout;
    private readonly IDelegatingHandlerHandlerFactory _factory;
    private readonly IOcelotLogger _logger;
    private DownstreamRoute _cacheKey;
    private IHttpClient _client;
    private HttpClient _httpClient;

    public CustomHttpClientBuilder(
        IDelegatingHandlerHandlerFactory factory,
        IHttpClientCache cacheHandlers,
        IOcelotLogger logger,
        ICustomOcelotMessageHandler customOcelotMessageHandler)
    {
        _factory = factory;
        _cacheHandlers = cacheHandlers;
        _logger = logger;

        // This is hardcoded at the moment but can easily be added to configuration
        // if required by a user request.
        _defaultTimeout = TimeSpan.FromSeconds(90);
        _customOcelotMessageHandler = customOcelotMessageHandler;
    }

    public IHttpClient Create(DownstreamRoute downstreamRoute)
    {
        _cacheKey = downstreamRoute;

        var httpClient = _cacheHandlers.Get(_cacheKey);

        if (httpClient != null)
        {
            _client = httpClient;
            return httpClient;
        }

        var handler = CreateHandler(downstreamRoute);

        if (handler is HttpClientHandler && downstreamRoute.DangerousAcceptAnyServerCertificateValidator)
        {
            ((HttpClientHandler)handler).ServerCertificateCustomValidationCallback = (request, certificate, chain, errors) => true;

            _logger
                .LogWarning($"You have ignored all SSL warnings by using DangerousAcceptAnyServerCertificateValidator for this DownstreamRoute, UpstreamPathTemplate: {downstreamRoute.UpstreamPathTemplate}, DownstreamPathTemplate: {downstreamRoute.DownstreamPathTemplate}");
        }

        var timeout = downstreamRoute.QosOptions.TimeoutValue == 0
            ? _defaultTimeout
            : TimeSpan.FromMilliseconds(downstreamRoute.QosOptions.TimeoutValue);

        _httpClient = new HttpClient(CreateHttpMessageHandler(handler, downstreamRoute))
        {
            Timeout = timeout
        };

        _client = new HttpClientWrapper(_httpClient);

        return _client;
    }

    public void Save()
    {
        _cacheHandlers.Set(_cacheKey, _client, TimeSpan.FromHours(24));
    }

    private HttpMessageHandler CreateHandler(DownstreamRoute downstreamRoute)
    {
        var wafHandler = _customOcelotMessageHandler.Gethandler(downstreamRoute);
        if (wafHandler != null)
        {
            return wafHandler; // waf routing, if not defined searches real ip/port below
        }

        // Dont' create the CookieContainer if UseCookies is not set or the HttpClient will complain
        // under .Net Full Framework
        var useCookies = downstreamRoute.HttpHandlerOptions.UseCookieContainer;
        return useCookies ? UseCookiesHandler(downstreamRoute) : UseNonCookiesHandler(downstreamRoute);
    }

    private HttpMessageHandler CreateHttpMessageHandler(HttpMessageHandler httpMessageHandler, DownstreamRoute request)
    {
        //todo handle error
        var handlers = _factory.Get(request).Data;

        handlers
            .Select(handler => handler)
            .Reverse()
            .ToList()
            .ForEach(handler =>
            {
                var delegatingHandler = handler();
                delegatingHandler.InnerHandler = httpMessageHandler;
                httpMessageHandler = delegatingHandler;
            });
        return httpMessageHandler;
    }

    private static HttpClientHandler UseCookiesHandler(DownstreamRoute downstreamRoute)
    {
        return new HttpClientHandler
        {
            AllowAutoRedirect = downstreamRoute.HttpHandlerOptions.AllowAutoRedirect,
            UseCookies = downstreamRoute.HttpHandlerOptions.UseCookieContainer,
            UseProxy = downstreamRoute.HttpHandlerOptions.UseProxy,
            MaxConnectionsPerServer = downstreamRoute.HttpHandlerOptions.MaxConnectionsPerServer,
            CookieContainer = new CookieContainer(),
        };
    }

    private static HttpClientHandler UseNonCookiesHandler(DownstreamRoute downstreamRoute)
    {
        return new HttpClientHandler
        {
            AllowAutoRedirect = downstreamRoute.HttpHandlerOptions.AllowAutoRedirect,
            UseCookies = downstreamRoute.HttpHandlerOptions.UseCookieContainer,
            UseProxy = downstreamRoute.HttpHandlerOptions.UseProxy,
            MaxConnectionsPerServer = downstreamRoute.HttpHandlerOptions.MaxConnectionsPerServer,
        };
    }
}