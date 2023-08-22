using Microsoft.Extensions.Logging;

namespace Application.Units.EventHandlers
{
    internal class UnitDeletedEventHandler: INotificationHandler<EntityDeletedEvent<ItemUnit>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<UnitDeletedEventHandler> _logger;

        public UnitDeletedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<UnitDeletedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityDeletedEvent<ItemUnit> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is deleted by user {userName} with id {id} at {dateTime}",
                typeof(ItemUnit).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
