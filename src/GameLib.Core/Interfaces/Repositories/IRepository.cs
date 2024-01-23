using System.Linq.Expressions;

namespace GameLib.Core.Interfaces.Repositories;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<IList<T>> GetAllAsync();
    Task<IList<T>> GetAsync(Expression<Func<T, bool>> predicate);
    
    void Add(T entity);
    Task<T> AddAndSaveAsync(T entity);
    void Update(T entity);
    Task<T> UpdateAndSaveAsync(T entity);
    Task<int> DeleteAsync(T entity);
    Task<int> DeleteAsync(int id);
}
