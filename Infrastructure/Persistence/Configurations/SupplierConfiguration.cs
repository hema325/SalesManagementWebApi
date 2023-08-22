using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(256);

            builder.OwnsMany(c => c.PhoneNumbers, builder =>
            {
                builder.Property(ph => ph.Value).HasMaxLength(24);
            });

            builder.HasIndex(c => c.Name).IsUnique(false);
        }
    }
}
