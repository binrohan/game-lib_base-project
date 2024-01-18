
using GameLib.Core.Dtos;
using GameLib.Core.Interfaces.Services;
using GameLib.Core.Responses;
using GameLib.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GameLib.API.Controllers;

public class GenresController(IGenreService service) : ApiController
{
    private readonly IGenreService _service = service;

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> Create([FromBody] GenreToCreateDto genreToCreate)
    {
        var genre = await _service.AddAndSaveAsync(genreToCreate);

        return Result.Created(genre);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetById(int id)
    {
        var genre = await _service.GetByIdAsync(id);

        return Result.Ok(genre);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetAll()
    {
        var genres = await _service.GetAllAsync();

        return Result.Ok(genres);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse>> Update([FromRoute] int id, [FromBody] GenreToUpdateDto genreToUpdate)
    {
        var entity = await _service.UpdateAndSaveAsync(id, genreToUpdate);

        return Result.Updated(entity);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> Delete([FromRoute] int id)
    {
        await _service.DeleteAsync(id);

        return Result.Deleted();
    }
}
