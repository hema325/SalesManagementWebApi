using Domain.Common.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    internal static class AuditableEntityConfiguration
    {
        public static void ConfigureAuditableEntity(ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes().Where(e => e.ClrType.IsAssignableTo(typeof(AuditableEntity))))
                foreach (var prop in entity.GetProperties().Where(e => e.ClrType == typeof(string)))
                    prop.SetMaxLength(450);
        }
    }
}
