using GameLib.API.Controllers;
using GameLib.Core.Dtos;
using GameLib.Core.Interfaces;
using GameLib.Domain.Entities;

namespace GameLib.API;

public class GamesController(IGameService service) : CrudOperationController<Game, GameToCreateDto, GameToReturnDto, GameToUpdateDto>(service)
{
    private readonly IGameService _service = service;
}
