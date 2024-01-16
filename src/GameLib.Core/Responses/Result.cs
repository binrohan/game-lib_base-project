using GameLib.Core.Responses;

namespace GameLib.Core.Responses;

public class Result
{
    public static ApiResponse Ok(object details, string? message = null) => new(200, details, message);
    public static ApiResponse Created(object details, string? message = null) => new(201, details, message);
    public static ApiResponse Updated(object details, string? message = null) => new(204, details, message);
    public static ApiResponse Patched(object details, string? message = null) => new(205, details, message);
    public static ApiResponse BadRequest(string? message = null) => new(400, message);
    public static ApiResponse ValidationFailed(IEnumerable<string> errors, string? message = "Validation Failed") => new(400, errors, message);

    // So on...
}
