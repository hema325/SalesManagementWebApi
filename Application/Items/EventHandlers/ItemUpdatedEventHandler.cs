using Microsoft.Extensions.Logging;

namespace Application.Items.EventHandlers
{
    internal class ItemUpdatedEventHandler: INotificationHandler<EntityUpdatedEvent<Item>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<ItemUpdatedEventHandler> _logger;

        public ItemUpdatedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<ItemUpdatedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityUpdatedEvent<Item> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is updated by user {userName} with id {id} at {dateTime}",
                typeof(Item).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
