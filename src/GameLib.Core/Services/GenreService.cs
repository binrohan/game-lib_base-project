using AutoMapper;
using GameLib.Core.Dtos;
using GameLib.Core.Interfaces;
using GameLib.Core.Interfaces.Services;
using GameLib.Domain.Entities;

namespace GameLib.Core.Serivces;

public class GenreService(IUnitOfWork unitOfWork, IMapper mapper) 
  : CrudOpserationService<Genre, GenreToCreateDto, GenreToReturnDto, GenreToUpdateDto>(unitOfWork, mapper), 
    IGenreService
{

}
