using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace GameLib.API.Attributes;

[AttributeUsage(AttributeTargets.Class |
                AttributeTargets.Struct |
                AttributeTargets.Parameter |
                AttributeTargets.Property |
                AttributeTargets.Enum,
                AllowMultiple = false)]
public class SwaggerExampleAttribute(string example) : Attribute
{
    public string Example { get; set; } = example;
}

public class SwaggerSchemaExampleFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo != null)
        {
            var schemaAttribute = context.MemberInfo.GetCustomAttributes<SwaggerExampleAttribute>()
           .FirstOrDefault();
            if (schemaAttribute != null)
                ApplySchemaAttribute(schema, schemaAttribute);
        }
    }

    private void ApplySchemaAttribute(OpenApiSchema schema, SwaggerExampleAttribute schemaAttribute)
    {
        if (schemaAttribute.Example != null)
        {
            schema.Example = new Microsoft.OpenApi.Any.OpenApiString(schemaAttribute.Example);
        }
    }
}