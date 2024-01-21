using AutoMapper;
using GameLib.Core.Dtos;
using GameLib.Domain.Entities;

namespace GameLib.Core.MapperProfiles;

public class GameMapperProfiles : Profile
{
    public GameMapperProfiles()
    {
        CreateMap<GameToCreateDto, Game>();
        CreateMap<GameToUpdateDto, Game>();
        CreateMap<Game, GameToReturnDto>();
    }
}
