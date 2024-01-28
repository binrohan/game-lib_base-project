using Microsoft.AspNetCore.Mvc;

namespace GameLib.API.Responses;

public class Result
{
    public static ActionResult<ApiResponse<T>> Ok<T>(T details, string? message = null) where T : class => new OkObjectResult(new ApiResponse<T>(200, details, message));
    public static ActionResult<ApiResponse<T>> Created<T>(T details, string? message = null) where T : class => new CreatedResult("", new ApiResponse<T>(201, details, message));
    public static ActionResult<ApiResponse<T>> Updated<T>(T? details = null, string? message = null) where T : class => new OkObjectResult(new ApiResponse<T>(204, details, message));
    public static ActionResult<ApiResponse> Deleted() => new OkObjectResult(new ApiResponse(200, "Delete successed"));
}
