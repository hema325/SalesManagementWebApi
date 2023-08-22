using Microsoft.Extensions.Logging;

namespace Application.Suppliers.EventHandlers
{
    internal class SupplierDeletedEvent : INotificationHandler<EntityDeletedEvent<Supplier>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<SupplierDeletedEvent> _logger;

        public SupplierDeletedEvent(IDateTime dateTime, ICurrentUser currentUser, ILogger<SupplierDeletedEvent> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityDeletedEvent<Supplier> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is deleted by user {userName} with id {id} at {dateTime}",
                typeof(Supplier).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
