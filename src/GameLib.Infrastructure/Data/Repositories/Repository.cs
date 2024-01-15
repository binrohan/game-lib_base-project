using System.Linq.Expressions;
using GameLib.Core.Interaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate) 
        => await _dbSet.Where(predicate).ToListAsync();

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public async Task<int> AddAndSaveAsync(T entity)
    {
        _dbSet.Add(entity);
        return await SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<int> UpdateAndSaveAsync(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return await SaveChangesAsync();
    }

    public void Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);

        _dbSet.Remove(entity);
    }

    public void DeleteById(int id)
    {
        T? entityToDelete = _dbSet.Find(id);

        if (entityToDelete is null)
            throw new Exception($"{nameof(entityToDelete)} does not exist");
        
        _dbSet.Remove(entityToDelete);
    }

    public async Task<int> SaveChangesAsync()
    {
        var currentDateTime = DateTimeOffset.UtcNow;

        var addedEntities = _context.ChangeTracker.Entries()
            .Where(p => p.State == EntityState.Added).ToList();

        var modifiedEntities = _context.ChangeTracker.Entries()
            .Where(p => p.State == EntityState.Modified).ToList();

        foreach (var e in addedEntities)
        {
            e.Entity.GetType().GetProperty("CreatedBy")?.SetValue(e.Entity, 0);
            e.Entity.GetType().GetProperty("Created")?.SetValue(e.Entity, currentDateTime);
        }

        foreach (var e in modifiedEntities)
        {
            bool hasValueChanged = IsEntityModified(e);

            if (hasValueChanged)
            {
                e.Entity.GetType().GetProperty("LastModifiedBy")?.SetValue(e.Entity, 0);
                e.Entity.GetType().GetProperty("LastModified")?.SetValue(e.Entity, currentDateTime);
            }
        }

        return await _context.SaveChangesAsync();
    }

    private static bool IsEntityModified(EntityEntry e)
    {
        foreach (var prop in e.OriginalValues.Properties)
        {
            var originalValue = e.OriginalValues[prop]?.ToString();
            var currentValue = e.CurrentValues[prop]?.ToString();
            
            if (originalValue != currentValue)
                return true;
        }

        return false;
    }
}
