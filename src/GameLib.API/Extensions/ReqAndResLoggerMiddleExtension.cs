using GameLib.API.Middlewares;

namespace GameLib.API.Extensions;

public static class RequestLoggerMiddleExtension
{
    public static IApplicationBuilder UseRequestLogger(this IApplicationBuilder builder) => builder.UseMiddleware<RequestLoggerMiddleware>();
}
