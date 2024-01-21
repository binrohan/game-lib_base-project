using AutoMapper;
using GameLib.Core.Dtos;
using GameLib.Domain.Entities;

namespace GameLib.Core.MapperProfiles;

public class GenreMapperProfile : Profile
{
    public GenreMapperProfile()
    {
        CreateMap<GenreToCreateDto, Genre>();
        CreateMap<GenreToUpdateDto, Genre>();
        CreateMap<Genre, GenreToReturnDto>();
    }
}
