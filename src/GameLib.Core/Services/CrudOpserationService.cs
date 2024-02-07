using System.Linq.Expressions;
using AutoMapper;
using GameLib.Core.Exceptions;
using GameLib.Core.Interfaces;
using GameLib.Core.Interfaces.Repositories;
using GameLib.Core.Interfaces.Services;

namespace GameLib.Core.Serivces;

public class CrudOpserationService<TEntity, TCreateDto, TReturnDto, TUpdateDto>(IUnitOfWork unitOfWork, IMapper mapper)
    : ICrudOperationService<TEntity, TCreateDto, TReturnDto, TUpdateDto>
        where TEntity : class 
        where TCreateDto : class
        where TReturnDto : class
        where TUpdateDto : class
{
    protected readonly IMapper _mapper = mapper;
    protected readonly IRepository<TEntity> _repo = unitOfWork.Repository<TEntity>();
    protected Expression<Func<TEntity, object>>[] Includes = [];

    public virtual async Task<TReturnDto> AddAndSaveAsync(TCreateDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);

        entity = await _repo.AddAndSaveAsync(entity);

        return _mapper.Map<TReturnDto>(entity);
    }

    public virtual async Task<IEnumerable<TReturnDto>> GetAllAsync()
    {
        var entities = await _repo.GetAllAsync(Includes);

        return _mapper.Map<IEnumerable<TReturnDto>>(entities);
    }

    public virtual async Task<TReturnDto> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id, Includes) ?? throw new NotFoundException();

        return _mapper.Map<TReturnDto>(entity);
    }

    public virtual async Task<TReturnDto> UpdateAndSaveAsync(int id, TUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException();

        _mapper.Map(dto, entity);

        entity = await _repo.UpdateAndSaveAsync(entity);

        return _mapper.Map<TReturnDto>(entity);
    }

    public virtual async Task<int> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id) ?? throw new NotFoundException();

        return await _repo.DeleteAsync(entity);
    }
}
