using GameLib.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GameLib.API;

public class Result
{
    public static ActionResult<ApiResponse> Ok(object details, string? message = null) => new OkObjectResult(new ApiResponse(200, details, message));
    public static ActionResult<ApiResponse> Created(object details, string? message = null) => new CreatedResult("", new ApiResponse(201, details, message));
    public static ActionResult<ApiResponse> Updated(object? details = null, string? message = null) => new OkObjectResult(new ApiResponse(204, details, message));
    public static ActionResult<ApiResponse> Deleted() => new OkObjectResult(new ApiResponse(200, "Delete successed"));
}
