using Microsoft.Extensions.Logging;

namespace Application.Invoices.EventHandlers
{
    internal class InvoiceDeletedEventHandler: INotificationHandler<EntityDeletedEvent<Invoice>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<InvoiceDeletedEventHandler> _logger;

        public InvoiceDeletedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<InvoiceDeletedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityDeletedEvent<Invoice> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is deleted by user {userName} with id {id} at {dateTime}",
                typeof(Invoice).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
