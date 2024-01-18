using AutoMapper;
using GameLib.Core.Interfaces;
using GameLib.Core.Interfaces.Services;
using GameLib.Domain.Entities;

namespace GameLib.Core.Serivces;

public class GenreService(IUnitOfWork unitOfWork, IMapper mapper) 
  : CrudOpserationService<Genre, Genre, Genre, Genre>(unitOfWork, mapper), 
    IGenreService
{

}
