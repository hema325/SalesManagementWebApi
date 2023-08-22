using Microsoft.Extensions.Logging;

namespace Application.Stocks.EventHandlers
{
    internal class StockUpdatedEvent: INotificationHandler<EntityUpdatedEvent<Stock>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<StockUpdatedEvent> _logger;

        public StockUpdatedEvent(IDateTime dateTime, ICurrentUser currentUser, ILogger<StockUpdatedEvent> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityUpdatedEvent<Stock> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is updated by user {userName} with id {id} at {dateTime}",
                typeof(Stock).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
