using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Bat.AspNetCore;

public class BatSwaggerAuthenticateFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor)
        {
            var haveAuthenticateAttribute = context.ApiDescription.CustomAttributes().Any(x => x.GetType().Name.Contains("Authenticate"));
            if (haveAuthenticateAttribute && !context.ApiDescription.CustomAttributes().Any((a) => a is AllowAnonymousAttribute))
            {
                if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Token",
                    In = ParameterLocation.Header,
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Default = new OpenApiString("33159CFB-06DF-4007-8DFF-17F8D916D782")
                    },
                    Description = "Header Token For Authenticate Request",
                });
            }
        }
    }
}