namespace GameLib.API.Responses;

public static class ResponseMessages
{
    public const string Success200 = "Success";
    public const string Deleted200 = "Deleted";
    public const string Created201 = "Created";
    public const string Updated204 = "Updated";
    public const string Patched205 = "Patched";
    public const string BadRequest400 = "Bad Request";
    public const string ValidationFailed400 = "One or more validation failures have occurred";
    public const string Unauthorized401 = "Unauthorized";
    public const string NotFound404 = "Not Found";
    public const string NotAllowed405 = "Not Allowed";
    public const string NotAccepted406 = "Not Accepted";
    public const string FailedToSave418 = "Failed to Save";
    public const string InternalServerError500 = "Internal Server Error";

    public static string? Get(int code, string? fallbackMessage = null)
    {
        return code switch
        {
            200 => Success200,
            201 => Created201,
            204 => Updated204,
            205 => Patched205,
            400 => BadRequest400,
            401 => Unauthorized401,
            404 => NotFound404,
            405 => NotAllowed405,
            406 => NotAccepted406,
            418 => FailedToSave418,
            500 => InternalServerError500,
            _ => fallbackMessage
        };
    }
}
