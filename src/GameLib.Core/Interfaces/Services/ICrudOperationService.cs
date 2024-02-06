namespace GameLib.Core.Interfaces.Services;

public interface ICrudOperationService<TEntity, TCreateDto, TReturnDto, TUpdateDto>
{
    Task<TReturnDto> AddAndSaveAsync(TCreateDto dto);
    Task<IEnumerable<TReturnDto>> GetAllAsync();
    Task<TReturnDto> GetByIdAsync(int id);
    Task<TReturnDto> UpdateAndSaveAsync(int id, TUpdateDto dto);
    Task<int> DeleteAsync(int id);
}
