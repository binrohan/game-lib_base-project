namespace GameLib.API.Responses;

public class ApiResponse<T> : ApiResponse  where T : class
{
    public ApiResponse(int status, T? details, string? message = null)
    {
        Status = status;
        Message = message ?? ResponseMessages.Get(status);
        Details = details;
        IsError = DetectError(status);
    }

    public new T? Details { get; set; }
}