using GameLib.API.Responses;
using GameLib.API.Controllers;
using GameLib.Core.Dtos;
using GameLib.Core.Interfaces;
using GameLib.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GameLib.API;

public class GamesController(IGameService service) : CrudOperationController<Game, GameToCreateDto, GameToReturnDto, GameToUpdateDto>(service)
{
    private readonly IGameService _service = service;

    [HttpPut("update-genres")]
    public async Task<ActionResult<ApiResponse<GameToReturnDto>>> UpdateGenre(UpdateGenreDto dto)
    {
        var entity = await _service.UpdateGenre(dto);

        return Result.Updated(entity);
    }
}
