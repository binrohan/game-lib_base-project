using System.Linq.Expressions;

namespace GameLib.Core.Interfaces.Repositories;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsync<Tid>(Tid id, params Expression<Func<T, object>>[] includes);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    
    Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<IList<T>> GetAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    Task<IList<T>> GetAsync(Expression<Func<T, bool>> filter);
    
    void Add(T entity);
    Task<T> AddAndSaveAsync(T entity);

    void Update(T entity);
    Task<T> UpdateAndSaveAsync(T entity);
    
    Task<int> DeleteAsync(T entity);
    Task<int> DeleteAsync(int id);
}
