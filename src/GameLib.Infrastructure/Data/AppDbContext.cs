using System.Reflection;
using GameLib.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GameLib.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) 
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Publisher> Publishers => Set<Publisher>();
    public DbSet<GameGenre> GameGenres => Set<GameGenre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public Task<int> UpdateAuditAndSaveChangesAsync(CancellationToken cancellationToken = default)
    {
         var currentDateTime = DateTimeOffset.UtcNow;

        var addedEntities = ChangeTracker.Entries()
            .Where(p => p.State == EntityState.Added).ToList();

        var modifiedEntities = ChangeTracker.Entries()
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

        return base.SaveChangesAsync(cancellationToken);
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
