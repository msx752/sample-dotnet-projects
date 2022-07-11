using Samp.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGlobalStartupServices<AuthApplicationSettings>(builder.Configuration);

var app = builder.Build();

app.UseGlobalStartupConfigures(app.Environment);

app.Run();