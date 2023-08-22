using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(256);
            builder.Property(i => i.Price).HasPrecision(9, 2);
            builder.Property(i => i.Barcode).HasMaxLength(50).IsUnicode(false);

            builder.OwnsMany(i => i.Images, builder =>
            {
                builder.Property(i => i.Path).HasMaxLength(1000).IsUnicode(false);
            });

            builder.HasIndex(i => i.Name).IsUnique(false);
            builder.HasIndex(i => i.IsActive).IsUnique(false);
            builder.HasIndex(i=>i.Barcode).IsUnique(false);
        }
    }
}
