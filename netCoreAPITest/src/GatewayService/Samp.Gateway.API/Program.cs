using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Samp.Core.Extensions;
using Samp.Gateway.API;

var builder = WebApplication.CreateBuilder(args);

var isUseDockerOcelot = Environment.GetEnvironmentVariable("USEDOCKEROCELOT"); //for the debugging purposes
if (isUseDockerOcelot != null && isUseDockerOcelot == "true")
{
    builder.Configuration.AddJsonFile("ocelot.docker.json", false, true);
}
else
{
    builder.Configuration.AddJsonFile("ocelot.json", false, true);
}

builder.Services.AddGlobalStartupServices<GatewayApplicationSettings>(builder.Configuration);
builder.Services.AddOcelot();

var app = builder.Build();

app.UseGlobalStartupConfigures(app.Environment);
app.UseOcelot().GetAwaiter().GetResult();

app.Run();