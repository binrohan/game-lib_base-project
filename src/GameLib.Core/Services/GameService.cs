using AutoMapper;
using GameLib.Core.Dtos;
using GameLib.Core.Exceptions;
using GameLib.Core.Interfaces;
using GameLib.Domain.Entities;

namespace GameLib.Core.Serivces;

public class GameService(IUnitOfWork unitOfWork, IMapper mapper) 
    : CrudOpserationService<Game, GameToCreateDto, GameToReturnDto, GameToUpdateDto>(unitOfWork, mapper, g => g.Genres, g => g.Publisher), 
      IGameService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async override Task<Game> AddAndSaveAsync(GameToCreateDto dto)
    {
        var game = _mapper.Map<Game>(dto);

        game.Genres = await _unitOfWork.Repository<Genre>().GetAsync(g => dto.GenreIds.Contains(g.Id));

        return await _repo.AddAndSaveAsync(game);
    }

    public async override Task<Game> UpdateAndSaveAsync(int id, GameToUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id, _includes);

        if(entity is null) throw new NotFoundException();

        _mapper.Map(dto, entity);

        entity.Genres = await _unitOfWork.Repository<Genre>().GetAsync(g => dto.GenreIds.Contains(g.Id));

        return await _repo.UpdateAndSaveAsync(entity);
    }

    public async Task<GameToReturnDto> UpdateGenre(UpdateGenreDto dto)
    {
        var entity = await _repo.GetByIdAsync(dto.GameId, _includes);

        if(entity is null) throw new NotFoundException();

        entity.Genres = await _unitOfWork.Repository<Genre>().GetAsync(g => dto.ListofGenreId.Contains(g.Id));

        await _repo.UpdateAndSaveAsync(entity);

        var entityToReturn = _mapper.Map<GameToReturnDto>(entity);

        return entityToReturn;
    }
}
