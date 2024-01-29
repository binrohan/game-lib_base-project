using FluentValidation.Results;
using GameLib.API.Responses;
using GameLib.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GameLib.API;

public class ExceptionsController : ConfigController
{
    [HttpGet("not-found")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public ActionResult<ApiResponse> ThrowNotFoundException()
    {
        throw new NotFoundException();
    }

    [HttpGet("validation-failed")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public ActionResult<ApiResponse> ThrowUnauthorizedException()
    {
        throw new UnauthorizedAccessException();
    }

    [HttpGet("forbidden-access")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
    public ActionResult<ApiResponse> ThrowForbiddenAccessException()
    {
        throw new ForbiddenAccessException();
    }

    [HttpGet("internal-server-error")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<ApiResponse> ThrowInternalServerErrorException()
    {
        throw new NullReferenceException();
    }
}
