using System.Linq.Expressions;

namespace GameLib.Core.Interaces.Repositories;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
    
    void Add(T entity);
    Task<int> AddAndSaveAsync(T entity);
    void Update(T entity);
    Task<int> UpdateAndSaveAsync(T entity);
    void Delete(T entity);
    void DeleteById(int id);
    
    Task<int> SaveChangesAsync();
}
