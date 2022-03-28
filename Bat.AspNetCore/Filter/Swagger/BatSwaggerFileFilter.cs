using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bat.AspNetCore;

public class BatSwaggerFileFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileParameters = context.MethodInfo
            .GetParameters()
            .Where(p => p.ParameterType == typeof(FileUploadModel));

        foreach (var parameter in fileParameters)
        {
            operation.Parameters = new List<OpenApiParameter>
            {
                new OpenApiParameter
                {
                    Name = parameter.Name,
                    Schema = new OpenApiSchema
                    {
                        Type = "file",
                        Description = "upload file"
                    },
                }
            };
        }
    }
}