using GameLib.Core;
using GameLib.Core.Interfaces;
using GameLib.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GameLib.API.Controllers;

public class PublishersController(IPublisherService service) : ApiController
{
    private readonly IPublisherService _service = service;

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> Create([FromBody] PublisherToCreateDto dto)
    {
        var entity = await _service.AddAndSaveAsync(dto);
        
        return Result.Created(entity);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> Get()
    {
        var entities = await _service.GetAllAsync();

        return Result.Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> Get([FromRoute] int id)
    {
        var entity = await _service.GetByIdAsync(id);

        return Result.Ok(entity);
    }

    [HttpPost("{id}")]
    public async Task<ActionResult<ApiResponse>> Update([FromRoute] int id, [FromBody] PublisherToUpdateDto dto)
    {
        var entity = await _service.UpdateAndSaveAsync(id, dto);
        
        return Result.Updated(entity);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> Delete([FromRoute] int id)
    {
        await _service.DeleteAsync(id);

        return Result.Deleted();
    }
}
