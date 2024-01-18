namespace GameLib.Core.Interfaces.Services;

public interface ICrudOperationService<TEntity, TCreateDto, TReturnDto, TUpdateDto>
{
    Task<TEntity> AddAndSaveAsync(TCreateDto dto);
    Task<IEnumerable<TReturnDto>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> UpdateAndSaveAsync(int id, TUpdateDto dto);
    Task<int> DeleteAsync(int id);
}
