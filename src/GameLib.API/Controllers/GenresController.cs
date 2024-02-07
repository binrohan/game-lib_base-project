
using GameLib.Core.Dtos;
using GameLib.Core.Interfaces.Services;
using GameLib.Domain.Entities;

namespace GameLib.API.Controllers;

public class GenresController(ICrudOperationService<Genre, GenreToCreateDto, GenreToReturnDto, GenreToUpdateDto> service) 
    : CrudOperationController<Genre, GenreToCreateDto, GenreToReturnDto, GenreToUpdateDto>(service)
{
    
}
