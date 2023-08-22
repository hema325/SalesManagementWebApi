using Microsoft.Extensions.Logging;

namespace Application.Invoices.EventHandlers
{
    internal class InvoiceUpdatedEventHandler: INotificationHandler<EntityUpdatedEvent<Invoice>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<InvoiceUpdatedEventHandler> _logger;

        public InvoiceUpdatedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<InvoiceUpdatedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityUpdatedEvent<Invoice> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is updated by user {userName} with id {id} at {dateTime}",
                typeof(Invoice).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
