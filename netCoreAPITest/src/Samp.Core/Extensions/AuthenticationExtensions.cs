using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Samp.Core.AppSettings;
using Samp.Core.Results;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Samp.Core.Extensions
{
    public static class AuthenticationExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication((ao) => ao.DefaultChallengeScheme = ao.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, (options) =>
                    {
                        var config = configuration.Get<ApplicationSettings>();

                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.JWTOptions.AccessTokenSecret)),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ClockSkew = TimeSpan.Zero,
                        };
                        options.Events = new JwtBearerEvents()
                        {
                            OnChallenge = async context =>
                            {
                                context.HandleResponse();

                                var httpContext = context.HttpContext;
                                var routeData = httpContext.GetRouteData();
                                var actionContext = new ActionContext(httpContext, routeData, new ActionDescriptor());
                                var result = new UnauthorizedResponse();
                                await result.ExecuteResultAsync(actionContext);
                            }
                        };
                    });

            return services;
        }
    }
}