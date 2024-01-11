using System.Reflection;
using GameLib.Domain;
using GameLib.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
}
