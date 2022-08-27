namespace SampleProject.Tests.OcelotRedirections;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Requester;
using Ocelot.Responses;

public class CustomHttpClientHttpRequester : IHttpRequester
{
    private readonly IHttpClientCache _cacheHandlers;
    private readonly ICustomOcelotMessageHandler _customOcelotMessageHandler;
    private readonly IDelegatingHandlerHandlerFactory _factory;
    private readonly IOcelotLogger _logger;
    private readonly IExceptionToErrorMapper _mapper;

    public CustomHttpClientHttpRequester(IOcelotLoggerFactory loggerFactory,
        IHttpClientCache cacheHandlers,
        IDelegatingHandlerHandlerFactory factory,
        IExceptionToErrorMapper mapper,
        ICustomOcelotMessageHandler customOcelotMessageHandler)
    {
        _logger = loggerFactory.CreateLogger<CustomHttpClientHttpRequester>();
        _cacheHandlers = cacheHandlers;
        _factory = factory;
        _mapper = mapper;
        _customOcelotMessageHandler = customOcelotMessageHandler;
    }

    public async Task<Response<HttpResponseMessage>> GetResponse(HttpContext httpContext)
    {
        var builder = new CustomHttpClientBuilder(_factory, _cacheHandlers, _logger, _customOcelotMessageHandler);

        var downstreamRoute = httpContext.Items.DownstreamRoute();

        var downstreamRequest = httpContext.Items.DownstreamRequest();

        var httpClient = builder.Create(downstreamRoute);

        try
        {
            var response = await httpClient.SendAsync(downstreamRequest.ToHttpRequestMessage(), httpContext.RequestAborted).ConfigureAwait(false);
            return new OkResponse<HttpResponseMessage>(response);
        }
        catch (Exception exception)
        {
            var error = _mapper.Map(exception);
            return new ErrorResponse<HttpResponseMessage>(error);
        }
        finally
        {
            builder.Save();
        }
    }
}