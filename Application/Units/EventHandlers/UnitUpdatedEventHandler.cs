using Microsoft.Extensions.Logging;

namespace Application.Units.EventHandlers
{
    internal class UnitUpdatedEventHandler: INotificationHandler<EntityUpdatedEvent<ItemUnit>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<UnitUpdatedEventHandler> _logger;

        public UnitUpdatedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<UnitUpdatedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityUpdatedEvent<ItemUnit> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is updated by user {userName} with id {id} at {dateTime}",
                typeof(ItemUnit).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
