using Microsoft.Extensions.Logging;

namespace Application.Clients.EventHandlers
{
    internal class ClientUpdatedEventHandler : INotificationHandler<EntityUpdatedEvent<Client>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<ClientUpdatedEventHandler> _logger;

        public ClientUpdatedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<ClientUpdatedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityUpdatedEvent<Client> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is updated by user {userName} with id {id} at {dateTime}",
                typeof(Client).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }

    }
}
