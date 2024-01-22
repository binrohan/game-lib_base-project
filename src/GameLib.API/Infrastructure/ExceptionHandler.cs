using GameLib.Core.Exceptions;
using GameLib.Core.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace GameLib.API.Infrastructure;

public class ExceptionHandler : IExceptionHandler
{
    private readonly IHostEnvironment _env;
    private readonly ILogger<ExceptionHandler> _logger;
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

    public ExceptionHandler(IHostEnvironment env, ILogger<ExceptionHandler> logger)
    {
        _env = env;
        _logger = logger;
        _exceptionHandlers = new()
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            };
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        if (_exceptionHandlers.ContainsKey(exceptionType))
            await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
        else 
            await HandleOtherException(httpContext, exception);

        return true;
    }

    private async Task HandleValidationException(HttpContext httpContext, Exception ex)
    {
        var exception = (ValidationException)ex;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        await httpContext.Response.WriteAsJsonAsync(new ApiResponse(StatusCodes.Status400BadRequest,
                                                                    validationFailures: exception.Errors,
                                                                    exception.Message));
    }

    private async Task HandleNotFoundException(HttpContext httpContext, Exception ex)
    {
        var exception = (NotFoundException)ex;

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        await httpContext.Response.WriteAsJsonAsync(new ApiResponse(StatusCodes.Status404NotFound,
                                                                    exception.Message,
                                                                    "The specified resource was not found."));
    }

    private async Task HandleUnauthorizedAccessException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await httpContext.Response.WriteAsJsonAsync(new ApiResponse(StatusCodes.Status401Unauthorized, "Unauthorized"));
    }

    private async Task HandleForbiddenAccessException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        await httpContext.Response.WriteAsJsonAsync(new ApiResponse(StatusCodes.Status403Forbidden, "Forbidden"));
    }

    private async Task HandleOtherException(HttpContext httpContext, Exception ex)
    {
        _logger.LogError("Path: {Path}, Message: {Message} {ex}", httpContext.Request.Path, ex.Message, ex);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        var response = _env.IsDevelopment()
                ? new ApiResponse(StatusCodes.Status500InternalServerError, ex.StackTrace, ex.Message)
                : new ApiResponse(StatusCodes.Status500InternalServerError, details: null, "Internal Server Error");

        await httpContext.Response.WriteAsJsonAsync(response);
    }
}
