using System.Linq.Expressions;
using GameLib.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameLib.Infrastructure.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<T?> GetByIdAsync<Tid>(Tid id, params Expression<Func<T, object>>[] includes)
    {
        return await includes.Aggregate(_dbSet.AsQueryable(),
                                    (query, include) => query.Include(include))
                                                             .FirstOrDefaultAsync(x => EF.Property<Tid>(x, "Id")!.Equals(id));
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter,
                                              params Expression<Func<T, object>>[] includes)
        => await includes.Aggregate(_dbSet.AsQueryable(),
                                    (query, include) => query.Include(include))
                                                             .FirstOrDefaultAsync(filter);

    public async Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes) 
        => await includes.Aggregate(_dbSet.AsQueryable(),
                                    (query, include) => query.Include(include))
                                                             .ToListAsync();

    public async Task<IList<T>> GetAsync(Expression<Func<T, bool>> filter,
                                         params Expression<Func<T, object>>[] includes) 
        => await includes.Aggregate(_dbSet.Where(filter),
                                    (query, include) => query.Include(include))
                                                             .ToListAsync();

    public async Task<IList<T>> GetAsync(Expression<Func<T, bool>> filter) 
        => await _dbSet.Where(filter).ToListAsync();

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public async Task<T> AddAndSaveAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.UpdateAuditAndSaveChangesAsync();
        return entity;
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<T> UpdateAndSaveAsync(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;

        await _context.UpdateAuditAndSaveChangesAsync();

        return entity;
    }

    public async Task<int> DeleteAsync(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);

        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(int id)
    {
        T? entityToDelete = _dbSet.Find(id);

        if (entityToDelete is null)
            throw new Exception($"{nameof(entityToDelete)} does not exist");
        
        _dbSet.Remove(entityToDelete);
        return await _context.SaveChangesAsync();
    }
}
