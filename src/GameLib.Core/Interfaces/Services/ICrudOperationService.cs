namespace GameLib.Core.Interfaces.Services;

public interface ICrudOperationService<TEntity, TCreateDto, TReturnDto, TUpdateDto>
{
    Task<TEntity> AddAndSaveAsync(TCreateDto dto);
    Task<IEnumerable<TReturnDto>> GetAllAsync();
    Task<TReturnDto> GetById(int id);
    Task<TEntity> UpdateAndSaveAsync(int id, TUpdateDto dto);
    Task<int> Delete(int id);
}
