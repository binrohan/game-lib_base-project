using GameLib.Core.Interfaces.Services;
using GameLib.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GameLib.API.Controllers;

public class CrudOperationController<TEntity, TCreateDto, TReturnDto, TUpdateDto>(ICrudOperationService<TEntity, TCreateDto, TReturnDto, TUpdateDto> service) 
    : ConfigController
        where TEntity : class 
        where TCreateDto : class 
        where TReturnDto : class 
        where TUpdateDto : class
{
    private readonly ICrudOperationService<TEntity, TCreateDto, TReturnDto, TUpdateDto> _service = service;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public virtual async Task<ActionResult<ApiResponse<TEntity>>> Create([FromBody] TCreateDto dto)
    {
        var entity = await _service.AddAndSaveAsync(dto);
        
        return Result.Created(entity);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public virtual async Task<ActionResult<ApiResponse<IEnumerable<TReturnDto>>>> Get()
    {
        var entities = await _service.GetAllAsync();

        return Result.Ok(entities);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult<ApiResponse<TReturnDto>>> Get([FromRoute] int id)
    {
        var entity = await _service.GetByIdAsync(id);

        return Result.Ok(entity);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<ApiResponse<TEntity>>> Update([FromRoute] int id, [FromBody] TUpdateDto dto)
    {
        var entity = await _service.UpdateAndSaveAsync(id, dto);
        
        return Result.Updated(entity);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult<ApiResponse>> Delete([FromRoute] int id)
    {
        await _service.DeleteAsync(id);

        return Result.Deleted();
    }
}
