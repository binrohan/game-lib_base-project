using FluentValidation.Results;
using GameLib.API.Responses;
using GameLib.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GameLib.API;

public class ExceptionsController : ConfigController
{
    [HttpGet("not-found")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ApiResponse> ThrowNotFoundException()
    {
        throw new NotFoundException();
    }

    [HttpGet("validation-failed")]
    public ActionResult<ApiResponse> ThrowValidationException()
    {
        ValidationFailure[] failures =
        [
            new ValidationFailure("Title", "Required"),
            new ValidationFailure("Title", "Maximum length is 100"),
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
