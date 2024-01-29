using GameLib.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GameLib.API;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
public class ConfigController : ControllerBase
{

}
