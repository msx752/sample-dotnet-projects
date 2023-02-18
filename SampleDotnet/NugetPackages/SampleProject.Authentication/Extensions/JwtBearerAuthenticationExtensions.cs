using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SampleProject.Authentication.Extensions
{
    public static class JwtBearerAuthenticationExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration, IActionResult unauthorizedResult = null)
        {
            services.AddAuthentication((ao) => ao.DefaultChallengeScheme = ao.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, (options) =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(configuration["JwtBearerOptions:Secret"])),
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
                                IActionResult result = unauthorizedResult ?? new UnauthorizedResult();
                                await result.ExecuteResultAsync(actionContext);
                            }
                        };
                    });

            return services;
        }
    }
}