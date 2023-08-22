using Microsoft.Extensions.Logging;

namespace Application.Suppliers.EventHandlers
{
    internal class SupplierUpdatedEvent: INotificationHandler<EntityUpdatedEvent<Supplier>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<SupplierUpdatedEvent> _logger;

        public SupplierUpdatedEvent(IDateTime dateTime, ICurrentUser currentUser, ILogger<SupplierUpdatedEvent> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityUpdatedEvent<Supplier> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is updated by user {userName} with id {id} at {dateTime}",
                typeof(Supplier).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
