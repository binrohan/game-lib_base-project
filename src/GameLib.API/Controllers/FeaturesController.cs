using GameLib.API.Responses;
using GameLib.Core.Dtos;
using GameLib.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GameLib.API;

public class FeaturesController : ConfigController
{
    [HttpGet("performance-monitor-middleware-test")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLateResponse()
    {
        await Task.Delay(700);

        return Ok();
    }

    // [HttpGet("returns-only-ok-swagger-test")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // public ActionResult ApiResponse()
    // {
    //     return Ok();
    // }

    // [HttpGet("returns-only-api-response-swagger-test")]
    // [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    // public ActionResult OnlyApiResponse()
    // {
    //     return Ok();
    // }

    // [HttpGet("returns-api-response-with-object-swagger-test")]
    // [ProducesResponseType(typeof(ApiResponse<GameToReturnDto>), StatusCodes.Status200OK)]
    // public ActionResult ApiResponseWithObject()
    // {
    //     return Ok();
    // }

    // [HttpGet("returns-api-response-with-list-of-object-swagger-test")]
    // [ProducesResponseType(typeof(ApiResponse<IEnumerable<string, string>>), StatusCodes.Status200OK)]
    // public ActionResult ApiResponseWithListOfObject()
    // {
    //     return Ok();
    // }

    // [HttpGet("returns-api-response-dictionary-swagger-test")]
    // [ProducesResponseType(typeof(ApiResponse<IDictionary<string, string>>), StatusCodes.Status200OK)]
    // public ActionResult ApiResponseWithListOfObject()
    // {
    //     return Ok();
    // }

    // [HttpGet("returns-api-response-object-swagger-test")]
    // [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    // public ActionResult ApiResponseWithListOfObject()
    // {
    //     return Ok();
    // }
}
