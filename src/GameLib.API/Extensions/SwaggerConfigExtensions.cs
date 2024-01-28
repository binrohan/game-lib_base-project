using System.Reflection;
using GameLib.API.Attributes;
using Microsoft.OpenApi.Models;

namespace GameLib.API.Extensions;

public static class SwaggerConfigExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(option => 
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "GameLib",
                Description = "This is the base project for leaning anything new and try",
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            option.IncludeXmlComments(xmlPath);
            option.SchemaFilter<SwaggerSchemaExampleFilter>();
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder builder)
    {
        builder.UseSwagger();
        builder.UseSwaggerUI(option =>
        {
            option.SwaggerEndpoint("v1/swagger.json", "GameLib v1");
        });

        return builder;
    }
}
