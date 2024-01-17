using GameLib.Core.Interfaces.Repositories;

namespace GameLib.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
}
