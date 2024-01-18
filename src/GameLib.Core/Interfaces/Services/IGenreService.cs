using GameLib.Core.Dtos;
using GameLib.Domain.Entities;

namespace GameLib.Core.Interfaces.Services;

public interface IGenreService : ICrudOperationService<Genre, GenreToCreateDto, GenreToListDto, GenreToUpdateDto>
{

}
