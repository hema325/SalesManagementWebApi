using Microsoft.Extensions.Logging;

namespace Application.Clients.EventHandlers
{
    internal class ClientDeletedEventHandler : INotificationHandler<EntityDeletedEvent<Client>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<ClientDeletedEventHandler> _logger;

        public ClientDeletedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<ClientDeletedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityDeletedEvent<Client> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is deleted by user {userName} with id {id} at {dateTime}",
                typeof(Client).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }

    }
}
