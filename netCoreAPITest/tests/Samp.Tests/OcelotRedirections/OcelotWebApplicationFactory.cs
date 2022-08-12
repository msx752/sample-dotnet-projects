namespace Samp.Tests.OcelotRedirections;

using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

public class OcelotWebApplicationFactory<TStartup>
         : WebApplicationFactory<TStartup> where TStartup : class
{
    private readonly HttpClient[] httpClients;

    public OcelotWebApplicationFactory(HttpClient[] httpClients)
    {
        this.httpClients = httpClients;
    }

    public OcelotWebApplicationFactory()
    {
    }

    protected override IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder(System.Array.Empty<string>()).ConfigureWebHostDefaults((wb) =>
        {
            wb.UseEnvironment("Development");
            wb.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("ocelot.json", false, true);
            });
            wb.UseStartup<TStartup>();
            wb.ConfigureServices(services =>
            {
                services.AddOcelotWAFRedirections(httpClients);
            });
        });
    }
}