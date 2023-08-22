using Microsoft.Extensions.Logging;

namespace Application.Units.EventHandlers
{
    internal class UnitCreatedEventHandler: INotificationHandler<EntityCreatedEvent<ItemUnit>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<UnitCreatedEventHandler> _logger;

        public UnitCreatedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<UnitCreatedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityCreatedEvent<ItemUnit> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is Created by user {userName} with id {id} at {dateTime}",
               typeof(ItemUnit).FullName,
               notification.Entity.Id,
               _currentUser.UserName,
               _currentUser.Id,
               _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
