using GameLib.Domain;
using GameLib.Domain.Entities;
using GameLib.Infrastructure.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameLib.Infrastructure;

public class GameConfig : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.Property(g => g.Title)
               .HasMaxLength(DataSchemeConstants.DEFAULT_NAME_LENGTH)
               .IsRequired();

        builder.HasMany(g => g.Genres)
               .WithMany(g => g.Games)
               .UsingEntity<GameGenre>();
    }
}
