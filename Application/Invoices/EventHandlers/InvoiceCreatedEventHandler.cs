using Microsoft.Extensions.Logging;

namespace Application.Invoices.EventHandlers
{
    internal class InvoiceCreatedEventHandler: INotificationHandler<EntityCreatedEvent<Invoice>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<InvoiceCreatedEventHandler> _logger;

        public InvoiceCreatedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<InvoiceCreatedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityCreatedEvent<Invoice> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is Created by user {userName} with id {id} at {dateTime}",
               typeof(Invoice).FullName,
               notification.Entity.Id,
               _currentUser.UserName,
               _currentUser.Id,
               _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
