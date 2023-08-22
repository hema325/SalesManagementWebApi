using Microsoft.Extensions.Logging;

namespace Application.Clients.EventHandlers
{
    internal class ClientCreatedEventHandler : INotificationHandler<EntityCreatedEvent<Client>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<ClientDeletedEventHandler> _logger;

        public ClientCreatedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<ClientDeletedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityCreatedEvent<Client> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is Created by user {userName} with id {id} at {dateTime}",
               typeof(Client).FullName,
               notification.Entity.Id,
               _currentUser.UserName,
               _currentUser.Id,
               _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
