namespace GameLib.API.Responses;

public class ApiResponse
{
    public ApiResponse() { }

    public ApiResponse(int status, string? message = null)
    {
        Status = status;
        Message = message ?? ResponseMessages.Get(status);
        IsError = DetectError(status);
    }

    public ApiResponse(int status, IList<string> validationFailures, string? message = null)
    {
        Status = status;
        Message = message ?? ResponseMessages.Get(status);
        IsError = DetectError(status);
        ValidationFailures = validationFailures;
    }

    public int Status { get; set; }
    public string? Message { get; set; }
    public bool IsError { get; protected set; }
    public object? Details { get; set; }
    public IList<string>? ValidationFailures { get; set; }

    protected static bool DetectError(int DetectError)
    {
        return DetectError > 299 || 200 > DetectError;
    }
}
