﻿namespace GameLib.Core.Responses;

public class ApiResponse<T> : ApiResponse  where T : class
{
    public ApiResponse(int status, T? details, string? message = null)
    {
        Status = status;
        Message = message ?? GetDefaultMessageForStatusCode(status);
        Details = details;
        IsError = DetectError(status);
    }

    public new T? Details { get; set; }
}
