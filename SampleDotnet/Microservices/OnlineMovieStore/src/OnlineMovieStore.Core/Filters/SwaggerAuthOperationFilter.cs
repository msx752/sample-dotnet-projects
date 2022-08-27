using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace SampleProject.Core.Filters
{
    public class SwaggerAuthOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation openApiOperation, OperationFilterContext operationFilterContext)
        {
            var customAttrs = operationFilterContext.ApiDescription.CustomAttributes();
            var isAuthorized = customAttrs.Any(attr => attr is IAuthorizeData);
            var isAllowAnonymous = customAttrs.Any(attr => attr is IAllowAnonymous);

            if (isAuthorized && !isAllowAnonymous)
            {
                openApiOperation.Parameters ??= new List<OpenApiParameter>();
                openApiOperation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "JWT access token",
                });

                openApiOperation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                openApiOperation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                openApiOperation.Security = new List<OpenApiSecurityRequirement>();
                openApiOperation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
            }
        }
    }
}