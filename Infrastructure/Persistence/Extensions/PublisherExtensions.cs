using Domain.Common.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Extensions
{
    internal static class PublisherExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IPublisher publisher, DbContext? context)
        {
            var entities = context?.ChangeTracker.Entries<EntityBase>().Where(e => e.Entity.DomainEvents.Any()).Select(e => e.Entity).ToList();
            var domainEvents = entities.SelectMany(e => e.DomainEvents);

            await Task.WhenAll(domainEvents.Select(e => publisher.Publish(e)));
            entities.ForEach(e => e.ClearDomainEvents());
        }
    }
}
