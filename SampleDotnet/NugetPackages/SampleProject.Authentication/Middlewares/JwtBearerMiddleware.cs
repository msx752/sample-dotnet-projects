using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleProject.Authentication.Middlewares
{
    public class JwtBearerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration configuration;

        public JwtBearerMiddleware(RequestDelegate next
            , IConfiguration configuration)
        {
            _next = next;
            this.configuration = configuration;
        }

        public virtual IEnumerable<string> ConfigureRoles(HttpContext httpContext, System.Security.Principal.GenericIdentity genericIdentity)
        {
            return Enumerable.Empty<string>();
        }

        public virtual void ConfigureTokenValidationParameters(ref TokenValidationParameters tokenValidationParameters, Span<byte> secretSpan)
        {
            tokenValidationParameters.ValidateIssuerSigningKey = true;
            tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(secretSpan.ToArray());
            tokenValidationParameters.ValidateIssuer = false;
            tokenValidationParameters.ValidateAudience = false;
            tokenValidationParameters.ClockSkew = TimeSpan.Zero;
        }

        public virtual void ConfigureUser(HttpContext httpContext, System.Security.Principal.GenericPrincipal genericPrincipal)
        {
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Substring(6);

            if (token != null)
                BindUser(context, token);

            await _next(context);
        }

        private void BindUser(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters();

                var keySpan = Encoding.Unicode.GetBytes(configuration["JwtBearerOptions:Secret"]).AsSpan();

                ConfigureTokenValidationParameters(ref tokenValidationParameters, keySpan);

                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                Claims(context, ((JwtSecurityToken)validatedToken).Claims);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }

        private void Claims(HttpContext httpContext, IEnumerable<Claim> claims)
        {
            string userId = claims.First(x => x.Type == "id").Value;
            var genericIdentity = new System.Security.Principal.GenericIdentity(userId);

            for (int i = 0; i < claims.Count(); i++)
                genericIdentity.AddClaim(claims.ElementAt(i));

            var configureRoles = ConfigureRoles(httpContext, genericIdentity);

            var user = new System.Security.Principal.GenericPrincipal(genericIdentity, configureRoles?.ToArray());
            ConfigureUser(httpContext, user);
            httpContext.User = user;
        }
    }
}