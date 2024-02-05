using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GameLib.API.Infrastructure.Swagger;

public class ProducesResponseTypeAttributeFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attribute = context.MethodInfo.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), false)?.FirstOrDefault();

        if (attribute is not ProducesResponseTypeAttribute)
            return;

        if (operation.Responses.ContainsKey(StatusCodes.Status500InternalServerError.ToString()))
            operation.Responses[StatusCodes.Status500InternalServerError.ToString()].Content = GetErrorReponseExample("Internal Server Error.",
                                                                                                                      StatusCodes.Status500InternalServerError);
        
        if (operation.Responses.ContainsKey(StatusCodes.Status400BadRequest.ToString()))
            operation.Responses[StatusCodes.Status400BadRequest.ToString()].Content = GetValidationFailureReponseExample();

        if (operation.Responses.ContainsKey(StatusCodes.Status404NotFound.ToString()))
            operation.Responses[StatusCodes.Status404NotFound.ToString()].Content = GetErrorReponseExample("The specified resource was not found.", 
                                                                                                           StatusCodes.Status404NotFound);

        if (operation.Responses.ContainsKey(StatusCodes.Status401Unauthorized.ToString()))
            operation.Responses[StatusCodes.Status401Unauthorized.ToString()].Content = GetErrorReponseExample("Unauthorized.",
                                                                                                               StatusCodes.Status401Unauthorized);

        if (operation.Responses.ContainsKey(StatusCodes.Status403Forbidden.ToString()))
            operation.Responses[StatusCodes.Status403Forbidden.ToString()].Content = GetErrorReponseExample("Forbidden.",
                                                                                                               StatusCodes.Status403Forbidden);
    }

    private static IDictionary<string, OpenApiMediaType> GetErrorReponseExample(string message, int errorCode)
    {
        return new Dictionary<string, OpenApiMediaType>
        {
            {
                "application/json", new OpenApiMediaType()
                {
                    Example = new OpenApiObject
                    {
                        ["status"] = new OpenApiInteger(errorCode),
                        ["message"] = new OpenApiString(message),
                        ["isError"] = new OpenApiBoolean(true),
                        ["details"] = new OpenApiNull(),
                        ["validationFailures"] = new OpenApiNull()
                    }
                }
            }
        };
    }

    private static IDictionary<string, OpenApiMediaType> GetValidationFailureReponseExample()
    {
        return new Dictionary<string, OpenApiMediaType>
        {
            {
                "application/json", new OpenApiMediaType()
                {
                    Example = new OpenApiObject
                    {
                        ["status"] = new OpenApiInteger(StatusCodes.Status400BadRequest),
                        ["message"] = new OpenApiString("One or more validation failures have occurred."),
                        ["isError"] = new OpenApiBoolean(true),
                        ["details"] = new OpenApiNull(),
                        ["validationFailures"] = new OpenApiArray()
                        {
                            new OpenApiString("Name can not be empty"),
                            new OpenApiString("Name can not exceed 100 characters"),
                            new OpenApiString("Phone Number in invalid format")
                        }
                    }
                }
            }
        };
    }
}

