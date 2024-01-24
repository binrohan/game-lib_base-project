using System.Diagnostics;

namespace GameLib.API.Middlewares;

public class PerformanceMonitorMiddleware(RequestDelegate next, ILogger<PerformanceMonitorMiddleware> logger, IHostEnvironment env)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<PerformanceMonitorMiddleware> _logger = logger;
    private readonly IHostEnvironment _env = env;
    private readonly Stopwatch _timer = new();

    public async Task InvokeAsync(HttpContext context)
    {
        _timer.Start();

        await _next(context);

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        var api = context.Request.Path;
        
        if (elapsedMilliseconds > 500)
            _logger.LogWarning("GameLib Long Running Request: {API} ({ElapsedMilliseconds} milliseconds)",
                api, elapsedMilliseconds);
        else
            _logger.LogWarning("GameLib Request Execution Time: {API} ({ElapsedMilliseconds} milliseconds)",
                api, elapsedMilliseconds);
    }
}
