using GameLib.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameLib.Infrastructure.Configs;

public class GenreConfig : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(g => g.Name)
               .HasMaxLength(DataSchemeConstants.DEFAULT_NAME_LENGTH)
               .IsRequired();
    }
}
