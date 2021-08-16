using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UrlLookupConfiguration : IEntityTypeConfiguration<UrlLookup>
    {
        public void Configure(EntityTypeBuilder<UrlLookup> builder)
        {
            builder.HasKey(e => e.Key);

            builder.Property(e => e.Key)
                   .HasMaxLength(8)
                   .IsRequired();

            builder.Property(e => e.Url)
                   .IsRequired();

            builder.HasIndex(e => e.Url)
                   .IsUnique();
        }
    }
}