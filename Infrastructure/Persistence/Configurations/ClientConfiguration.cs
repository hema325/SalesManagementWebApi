using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(256);
            builder.Property(c => c.DateOfBirth).HasColumnType("date");
            

            builder.OwnsMany(c => c.PhoneNumbers, builder =>
            {
                builder.Property(ph => ph.Value).HasMaxLength(24);
            });

            builder.OwnsMany(c => c.Images, builder =>
            {
                builder.Property(i => i.Path).HasMaxLength(1000).IsUnicode(false);
            });

            builder.HasIndex(c => c.Name).IsUnique(false);
        }
    }
}
