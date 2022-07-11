using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Samp.Core.Extensions;
using Samp.Gateway.API;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", false, true);

builder.Services.AddGlobalStartupServices<GatewayApplicationSettings>(builder.Configuration);
builder.Services.AddOcelot();

var app = builder.Build();

app.UseGlobalStartupConfigures(app.Environment);
app.UseOcelot().GetAwaiter().GetResult();

app.Run();