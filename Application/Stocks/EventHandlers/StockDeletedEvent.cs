using Microsoft.Extensions.Logging;

namespace Application.Stocks.EventHandlers
{
    internal class StockDeletedEvent: INotificationHandler<EntityDeletedEvent<Stock>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<StockDeletedEvent> _logger;

        public StockDeletedEvent(IDateTime dateTime, ICurrentUser currentUser, ILogger<StockDeletedEvent> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityDeletedEvent<Stock> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is deleted by user {userName} with id {id} at {dateTime}",
                typeof(Stock).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
