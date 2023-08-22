using Microsoft.Extensions.Logging;

namespace Application.Items.EventHandlers
{
    internal class ItemCreatedEventHandler: INotificationHandler<EntityCreatedEvent<Item>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<ItemCreatedEventHandler> _logger;

        public ItemCreatedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<ItemCreatedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityCreatedEvent<Item> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is Created by user {userName} with id {id} at {dateTime}",
               typeof(Item).FullName,
               notification.Entity.Id,
               _currentUser.UserName,
               _currentUser.Id,
               _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
