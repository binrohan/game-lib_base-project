﻿using GameLib.Core.Interfaces.Services;
using GameLib.Core.Responses;
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
    public virtual async Task<ActionResult<ApiResponse>> Create([FromBody] TCreateDto dto)
    {
        var entity = await _service.AddAndSaveAsync(dto);
        
        return Result.Created(entity);
    }

    [HttpGet]
    public virtual async Task<ActionResult<ApiResponse>> Get()
    {
        var entities = await _service.GetAllAsync();

        return Result.Ok(entities);
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<ApiResponse>> Get([FromRoute] int id)
    {
        var entity = await _service.GetByIdAsync(id);

        return Result.Ok(entity);
    }

    [HttpPost("{id}")]
    public virtual async Task<ActionResult<ApiResponse>> Update([FromRoute] int id, [FromBody] TUpdateDto dto)
    {
        var entity = await _service.UpdateAndSaveAsync(id, dto);
        
        return Result.Updated(entity);
    }

    [HttpDelete("{id}")]
    public virtual async Task<ActionResult<ApiResponse>> Delete([FromRoute] int id)
    {
        await _service.DeleteAsync(id);

        return Result.Deleted();
    }
}