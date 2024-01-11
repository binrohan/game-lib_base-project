using GameLib.Domain.Entities;
using GameLib.Infrastructure.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameLib.Infrastructure;

public class PublisherConfig : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.Property(p => p.Name)
               .HasMaxLength(DataSchemeConstants.DEFAULT_NAME_LENGTH)
               .IsRequired();
               
        builder.OwnsMany(g => g.PhoneNumbers);
    }
}
