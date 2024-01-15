namespace GameLib.Core.Responses;

public class ApiResponse
{
    public ApiResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    public ApiResponse(int statusCode, object? data, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        Data = data;
        IsError = DetectError(statusCode);
    }

    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public bool IsError { get; private set; }
    public object? Data { get; set; }

    private static string? GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            200 => "Success",
            201 => "Post Success",
            204 => "Update Success",
            205 => "Patch Success",
            400 => "Bad Request",
            401 => "Unauthorize",
            404 => "Not Found",
            405 => "Not Allowed",
            406 => "Not Accepted",
            418 => "Failed to Save",
            500 => "Internal Server Error",
            _ => null
        };
    }

    private static bool DetectError(int DetectError)
    {
        return DetectError > 299 || 200 > DetectError;
    }
}
