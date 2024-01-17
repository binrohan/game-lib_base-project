using System.Collections;
using GameLib.Core.Interfaces.Repositories;
using GameLib.Core.Interfaces;
using GameLib.Infrastructure.Data.Repositories;

namespace GameLib.Infrastructure;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private Hashtable? _repositories;

    public void Dispose()
    {
        _context.Dispose();
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        _repositories ??= [];

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (IRepository<TEntity>) _repositories[type]!;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
