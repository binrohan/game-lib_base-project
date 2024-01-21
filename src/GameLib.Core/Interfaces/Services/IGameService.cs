using GameLib.Core.Dtos;
using GameLib.Core.Interfaces.Services;
using GameLib.Domain.Entities;

namespace GameLib.Core.Interfaces;

public interface IGameService : ICrudOperationService<Game, GameToCreateDto, GameToReturnDto, GameToUpdateDto>
{

}
