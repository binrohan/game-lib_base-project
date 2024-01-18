using AutoMapper;
using GameLib.Core.Exceptions;
using GameLib.Core.Interfaces;
using GameLib.Core.Interfaces.Repositories;
using GameLib.Core.Interfaces.Services;

namespace GameLib.Core.Serivces;

public abstract class CrudOpserationService<TEntity, TCreateDto, TReturnDto, TUpdateDto>(IUnitOfWork unitOfWork, IMapper mapper) 
    : ICrudOperationService<TEntity, TCreateDto, TReturnDto, TUpdateDto> 
        where TEntity : class 
        where TCreateDto : class
        where TReturnDto : class
        where TUpdateDto : class
{
    protected readonly IMapper _mapper = mapper;
    protected readonly IRepository<TEntity> _repo = unitOfWork.Repository<TEntity>();

    public virtual async Task<TEntity> AddAndSaveAsync(TCreateDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);

        return await _repo.AddAndSaveAsync(entity);
    }

    public virtual async Task<IEnumerable<TReturnDto>> GetAllAsync()
    {
        var entities = await _repo.GetAllAsync();

        return _mapper.Map<IEnumerable<TReturnDto>>(entities);
    }

    public virtual async Task<TReturnDto> GetById(int id)
    {
        var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException();

        return _mapper.Map<TReturnDto>(entity);
    }

    public virtual async Task<TEntity> UpdateAndSaveAsync(int id, TUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException();

        _mapper.Map(dto, entity);

        return await _repo.UpdateAndSaveAsync(entity);
    }

    public virtual async Task<int> Delete(int id)
    {
        var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException();

        return await _repo.DeleteAsync(id);
    }
}
