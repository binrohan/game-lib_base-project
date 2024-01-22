namespace GameLib.API.Middlewares;

public class RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        LogRequestInformation(context.Request);

        await _next(context);
    }

    private void LogRequestInformation(HttpRequest request)
    {
        _logger.LogInformation("GameLib Request: {Method} {path}", request.Method, request.Path);
    }
}
