namespace GameLib.API.Responses;

public static class ResponseCodes
{
    public const int Success = 200;
    public const int Deleted = 200;
    public const int Created = 201;
    public const int Updated = 204;
    public const int Patched = 205;
    public const int BadRequest = 400;
    public const int Unauthorized = 401;
    public const int NotFound = 404;
    public const int NotAllowed = 405;
    public const int NotAccepted = 406;
    public const int FailedToSave = 418;
    public const int InternalServerError = 500;
}
