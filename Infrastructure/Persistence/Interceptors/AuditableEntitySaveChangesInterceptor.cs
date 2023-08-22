using Domain.Common.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors
{
    internal class AuditableEntitySaveChangesInterceptor: SaveChangesInterceptor
    {
        private readonly ICurrentUser _currentUser;
        private readonly IDateTime _dateTime;

        public AuditableEntitySaveChangesInterceptor(ICurrentUser currentUser, IDateTime dateTime, IPublisher publisher)
        {
            _currentUser = currentUser;
            _dateTime = dateTime;   
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var entities = eventData.Context.ChangeTracker.Entries();

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added && entity.Entity is AuditableEntity)
                {
                    var auditableEntity = (AuditableEntity)entity.Entity;

                    auditableEntity.CreatedOn = _dateTime.Now;
                    auditableEntity.CreatedBy = _currentUser.Id;

                    auditableEntity.ModifiedOn = _dateTime.Now;
                    auditableEntity.ModifiedBy = _currentUser.Id;
                }
                else if (entity.State == EntityState.Modified && entity.Entity is AuditableEntity)
                {
                    var auditableEntity = (AuditableEntity)entity.Entity;

                    auditableEntity.ModifiedOn = _dateTime.Now;
                    auditableEntity.ModifiedBy = _currentUser.Id;
                }
                else if (entity.State == EntityState.Deleted)
                {
                    if (entity.Entity is AuditableEntity auditableEntity)
                    {
                        entity.State = EntityState.Unchanged;

                        auditableEntity.DeletedOn = _dateTime.Now;
                        auditableEntity.DeletedBy = _currentUser.Id;

                    }
                    else if (entity.Metadata.IsOwned() && entity.Metadata.FindOwnership().PrincipalEntityType.ClrType.IsAssignableTo(typeof(AuditableEntity)))
                    {
                        entity.State = EntityState.Unchanged;
                    }
                }
            }

            return  base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
