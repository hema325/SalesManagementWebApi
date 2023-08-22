using Microsoft.Extensions.Logging;

namespace Application.Suppliers.EventHandlers
{
    internal class SupplierCreatedEvent : INotificationHandler<EntityCreatedEvent<Supplier>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<SupplierCreatedEvent> _logger;

        public SupplierCreatedEvent(IDateTime dateTime, ICurrentUser currentUser, ILogger<SupplierCreatedEvent> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityCreatedEvent<Supplier> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is Created by user {userName} with id {id} at {dateTime}",
               typeof(Supplier).FullName,
               notification.Entity.Id,
               _currentUser.UserName,
               _currentUser.Id,
               _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
