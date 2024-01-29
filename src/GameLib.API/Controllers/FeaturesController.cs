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
}
