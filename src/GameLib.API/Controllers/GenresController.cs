
using GameLib.Core.Dtos;
using GameLib.Core.Interfaces.Services;
using GameLib.Domain.Entities;

namespace GameLib.API.Controllers;

public class GenresController(IGenreService service) 
    : CrudOperationController<Genre, GenreToCreateDto, GenreToReturnDto, GenreToUpdateDto>(service)
{
    private readonly IGenreService _service = service;
}
