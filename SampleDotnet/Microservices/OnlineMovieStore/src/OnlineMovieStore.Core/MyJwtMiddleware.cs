using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SampleProject.Authentication.Middlewares;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace OnlineMovieStore.Core
{
    public class MyJwtMiddleware : JwtBearerMiddleware
    {
        public MyJwtMiddleware(RequestDelegate next, IConfiguration configuration)
            : base(next, configuration)
        {
        }

        public override IEnumerable<string> ConfigureRoles(HttpContext httpContext, GenericIdentity genericIdentity)
        {
            return new string[] { "user" };
        }

        public override void ConfigureUser(HttpContext httpContext, GenericPrincipal genericPrincipal)
        {
            var userId = genericPrincipal.Claims.First(x => x.Type == "id").Value;

            httpContext.Items["UserId"] = userId;
        }
    }
}