using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class UnitConfiguration : IEntityTypeConfiguration<ItemUnit>
    {
        public void Configure(EntityTypeBuilder<ItemUnit> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(256);

            builder.HasIndex(c => c.Name).IsUnique(false);
        }
    }
}
