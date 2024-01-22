using GameLib.API.Middlewares;

namespace GameLib.API.Extensions;

public static  class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder) => builder.UseMiddleware<ExceptionMiddleware>();
    public static IApplicationBuilder UseRequestLogger(this IApplicationBuilder builder) => builder.UseMiddleware<RequestLoggerMiddleware>();
    public static IApplicationBuilder UsePerformanceMonitor(this IApplicationBuilder builder) => builder.UseMiddleware<PerformanceMonitorMiddleware>();
}
