using FluentValidation.Results;
using GameLib.API.Controllers;
using GameLib.Core.Exceptions;
using GameLib.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GameLib.API;

public class ExceptionsController : ConfigController
{
    [HttpGet("not-found")]
    public ActionResult<ApiResponse> ThrowNotFoundException()
    {
        throw new NotFoundException();
    }

    [HttpGet("validation-failed")]
    public ActionResult<ApiResponse> ThrowValidationException()
    {
        ValidationFailure[] failures =
        [
            new ValidationFailure("Title", "Title is required"),
            new ValidationFailure("Title", "Title is required"),
            new ValidationFailure("PhoneNumber", "Phone number not in correct format")
        ];

        throw new ValidationException(failures);
    }

    [HttpGet("unauthorized")]
    public ActionResult<ApiResponse> ThrowUnauthorizedException()
    {
        throw new UnauthorizedAccessException();
    }

    [HttpGet("forbidden-access")]
    public ActionResult<ApiResponse> ThrowForbiddenAccessException()
    {
        throw new ForbiddenAccessException();
    }

    [HttpGet("internal-server-error")]
    public ActionResult<ApiResponse> ThrowInternalServerErrorException()
    {
        throw new NullReferenceException();
    }
}
