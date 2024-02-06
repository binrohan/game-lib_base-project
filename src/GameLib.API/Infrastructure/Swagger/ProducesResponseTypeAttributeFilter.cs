using System.Reflection;
using System.Text.Json;
using GameLib.API.Responses;
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

        if (attribute is not ProducesResponseTypeAttribute producesResponseTypeAttribute)
            return;

        // Exception
        if (operation.Responses.ContainsKey(StatusCodes.Status500InternalServerError.ToString()))
            operation.Responses[StatusCodes.Status500InternalServerError.ToString()].Content = GetErrorReponseExample(context, ResponseCodes.InternalServerError);

        if (operation.Responses.ContainsKey(StatusCodes.Status400BadRequest.ToString()))
            operation.Responses[StatusCodes.Status400BadRequest.ToString()].Content = GetValidationFailureReponseExample(context);

        if (operation.Responses.ContainsKey(StatusCodes.Status404NotFound.ToString()))
            operation.Responses[StatusCodes.Status404NotFound.ToString()].Content = GetErrorReponseExample(context, ResponseCodes.NotFound);

        if (operation.Responses.ContainsKey(StatusCodes.Status401Unauthorized.ToString()))
            operation.Responses[StatusCodes.Status401Unauthorized.ToString()].Content = GetErrorReponseExample(context, ResponseCodes.Unauthorized);

        if (operation.Responses.ContainsKey(StatusCodes.Status403Forbidden.ToString()))
            operation.Responses[StatusCodes.Status403Forbidden.ToString()].Content = GetErrorReponseExample(context, ResponseCodes.NotAllowed);

        // 200 series
        // if (operation.Responses.ContainsKey(StatusCodes.Status200OK.ToString()))
        // {
        //     Type type = producesResponseTypeAttribute.Type;

        //     if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ApiResponse<>))
        //         operation.Responses[StatusCodes.Status200OK.ToString()].Content = GetExampleForApiResponse(context, type.GetGenericArguments()[0]);

        //     if (type == typeof(ApiResponse))
        //         operation.Responses[StatusCodes.Status200OK.ToString()].Content = GetApiResponseExample(context, type, 200, "Success");
        // }
    }

    private static Dictionary<string, OpenApiMediaType> GetExampleForApiResponse(OperationFilterContext context, Type type)
    {
        var listTypes = new Type[] { typeof(IEnumerable<>), typeof(IList<>), typeof(List<>), typeof(IReadOnlyList<>)};
        var isCollectionType = type.IsGenericType && listTypes.Contains(type.GetGenericTypeDefinition());

        if (type.IsGenericType && isCollectionType)
            return GetApiResponseWithListExample(context, type.GetGenericArguments()[0], 200, "Success");
        
        var instance = GetObject(type);
        var instanceJSON = JsonSerializer.Serialize(instance);

        return GetApiResponseExample(context, type, 200, "Success", instanceJSON);
    }



    private static Dictionary<string, OpenApiMediaType> GetApiResponseWithListExample(OperationFilterContext context, Type elementType, int code, string message)
    {
        var innerInstance = Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
        MethodInfo? addMethod = innerInstance?.GetType().GetMethod("Add");
        var instance = GetObject(elementType);
        addMethod?.Invoke(innerInstance, [instance]);

        var instanceJSON = JsonSerializer.Serialize(innerInstance);
        return GetApiResponseExample(context, elementType, code, message, instanceJSON);
    }

    private static Dictionary<string, OpenApiMediaType> GetErrorReponseExample(OperationFilterContext context, int coded)
    {
        return new Dictionary<string, OpenApiMediaType>
        {
            {
                "application/json", new OpenApiMediaType()
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(ApiResponse), context.SchemaRepository),
                    Example = new OpenApiObject
                    {
                        ["status"] = new OpenApiInteger(coded),
                        ["message"] = new OpenApiString(ResponseMessages.Get(coded)),
                        ["isError"] = new OpenApiBoolean(true),
                        ["details"] = new OpenApiNull(),
                        ["validationFailures"] = new OpenApiNull()
                    }
                }
            }
        };
    }

    private static Dictionary<string, OpenApiMediaType> GetValidationFailureReponseExample(OperationFilterContext context)
    {
        return new Dictionary<string, OpenApiMediaType>
        {
            {
                "application/json", new OpenApiMediaType()
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(ApiResponse), context.SchemaRepository),
                    Example = new OpenApiObject
                    {
                        ["status"] = new OpenApiInteger(ResponseCodes.BadRequest),
                        ["message"] = new OpenApiString(ResponseMessages.ValidationFailed400),
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

    private static Dictionary<string, OpenApiMediaType> GetApiResponseExample(OperationFilterContext context, Type type, int code, string message, string? details = null)
    {
        return new Dictionary<string, OpenApiMediaType>
        {
            {
                "application/json", new OpenApiMediaType()
                {
                    Schema = context.SchemaGenerator.GenerateSchema(type, context.SchemaRepository),
                    Example = new OpenApiObject
                    {
                        ["status"] = new OpenApiInteger(code),
                        ["message"] = new OpenApiString(message),
                        ["isError"] = new OpenApiBoolean(false),
                        ["details"] = OpenApiAnyFactory.CreateFromJson(details),
                        ["validationFailures"] = new OpenApiNull()
                    }
                }
            }
        };
    }

    private static object GetObject(Type type)
    {
        ConstructorInfo constructor = type.GetConstructors()[0];
        ParameterInfo[] parameters = constructor.GetParameters();
        object?[] defaultValues2 = new object[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            defaultValues2[i] = GetDefaultValue(parameters[i].ParameterType);
        }

        return constructor.Invoke(defaultValues2);
    }

    private static object? GetDefaultValue(Type type)
    {
        System.Console.WriteLine(type);
        if (!type.IsValueType) return null;
        if (type == typeof(Int128) || type == typeof(long) || type == typeof(int) || type == typeof(short))
            return 69;

        return Activator.CreateInstance(type);
    }
}

