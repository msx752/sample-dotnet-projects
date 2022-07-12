using Microsoft.EntityFrameworkCore;
using Samp.Auth.Database;
using Samp.Core.Extensions;
using Samp.Core.Model;
using Samp.Identity.Core.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGlobalStartupServices<IdentityApplicationSettings>(builder.Configuration);

var IdentityContext = new DbContextParameter<SampIdentityContext, IdentityContextSeed>((provider, opt) =>
        opt.UseInMemoryDatabase(databaseName: "SampIdentitiyContext").EnableSensitiveDataLogging());

builder.Services.AddCustomDbContext(IdentityContext);

var app = builder.Build();

app.UseGlobalStartupConfigures(app.Environment);

app.Run();