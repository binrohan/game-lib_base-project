using AutoMapper;
using GameLib.Core.Dtos;
using GameLib.Core.Exceptions;
using GameLib.Core.Interfaces;
using GameLib.Core.Interfaces.Services;
using GameLib.Domain.Entities;

namespace GameLib.Core.Serivces;

public class GameService 
    : CrudOpserationService<Game, GameToCreateDto, GameToReturnDto, GameToUpdateDto>, 
      IGameService
{
    private readonly IUnitOfWork _unitOfWork;

    public GameService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _unitOfWork = unitOfWork;
        Includes = [g => g.Publisher, g => g.Genres];
    }

    public async override Task<GameToReturnDto> AddAndSaveAsync(GameToCreateDto dto)
    {
        var game = _mapper.Map<Game>(dto);

        game.Genres = await _unitOfWork.Repository<Genre>().GetAsync(g => dto.GenreIds.Contains(g.Id));

        game = await _repo.AddAndSaveAsync(game);

        return _mapper.Map<GameToReturnDto>(game);
    }

    public async override Task<GameToReturnDto> UpdateAndSaveAsync(int id, GameToUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id, Includes);

        if(entity is null) throw new NotFoundException();

        _mapper.Map(dto, entity);

        entity.Genres = await _unitOfWork.Repository<Genre>().GetAsync(g => dto.GenreIds.Contains(g.Id));

        entity = await _repo.UpdateAndSaveAsync(entity);

        return _mapper.Map<GameToReturnDto>(entity);
    }

    public async Task<GameToReturnDto> UpdateGenre(UpdateGenreDto dto)
    {
        var entity = await _repo.GetByIdAsync(dto.GameId, Includes);

        if(entity is null) throw new NotFoundException();

        entity.Genres = await _unitOfWork.Repository<Genre>().GetAsync(g => dto.ListofGenreId.Contains(g.Id));

        await _repo.UpdateAndSaveAsync(entity);

        var entityToReturn = _mapper.Map<GameToReturnDto>(entity);

        return entityToReturn;
    }
}
