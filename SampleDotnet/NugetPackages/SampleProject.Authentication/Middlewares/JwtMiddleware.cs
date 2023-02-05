using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SampleProject.Authentication.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration configuration;

        public JwtMiddleware(RequestDelegate next
            , IConfiguration configuration)
        {
            _next = next;
            this.configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Substring(6);

            if (token != null)
                attachUserToContext(context, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["JWT:AccessTokenSecret"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                context.User = new System.Security.Principal.GenericPrincipal(
                    new System.Security.Principal.GenericIdentity(userId),
                    new string[] { "user" });

                context.Items["UserId"] = Guid.Parse(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}