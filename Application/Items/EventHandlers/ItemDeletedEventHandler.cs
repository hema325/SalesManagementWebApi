using Microsoft.Extensions.Logging;

namespace Application.Items.EventHandlers
{
    internal class ItemDeletedEventHandler: INotificationHandler<EntityDeletedEvent<Item>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<ItemDeletedEventHandler> _logger;

        public ItemDeletedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<ItemDeletedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityDeletedEvent<Item> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is deleted by user {userName} with id {id} at {dateTime}",
                typeof(Item).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
