using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bat.AspNetCore;

public class BatSwaggerExcludeFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema?.Properties == null || context == null) return;
        if (context.Type.GetCustomAttribute(typeof(SwaggerExcludeAttribute), true) != null) schema.Properties = null;
    }
}