using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using netCoreAPI.Core.AppSettings;
using System;
using System.Text;

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
            var config = configuration.Get<ApplicationSettings>();
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = config.JWT.ValidAudience,
                ValidIssuer = config.JWT.ValidIssuer.ToString(),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JWT.AccessTokenSecret))
            };
            services.AddSingleton((isp) => tokenValidationParameters);

            services.AddAuthentication((ao) => ao.DefaultChallengeScheme = ao.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, (options) =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = tokenValidationParameters;
                    });

            return services;
        }
    }
}