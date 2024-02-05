namespace GameLib.API.Responses;

public class ApiResponse
{
    public ApiResponse()
    {
        
    }

    public ApiResponse(int status, string? message = null)
    {
        Status = status;
        Message = message ?? GetDefaultMessageForStatusCode(status);
        IsError = DetectError(status);
    }

    public ApiResponse(int status, IList<string> validationFailures, string? message = null)
    {
        Status = status;
        Message = message ?? GetDefaultMessageForStatusCode(status);
        IsError = DetectError(status);
        ValidationFailures = validationFailures;
    }

    public int Status { get; set; }
    public string? Message { get; set; }
    public bool IsError { get; protected set; }
    public object? Details { get; set; }
    public IList<string>? ValidationFailures { get; set; }

    protected static string? GetDefaultMessageForStatusCode(int status)
    {
        return status switch
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

    protected static bool DetectError(int DetectError)
    {
        return DetectError > 299 || 200 > DetectError;
    }
}
