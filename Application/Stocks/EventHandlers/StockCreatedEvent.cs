using Microsoft.Extensions.Logging;

namespace Application.Stocks.EventHandlers
{
    internal class StockCreatedEvent: INotificationHandler<EntityCreatedEvent<Stock>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<StockCreatedEvent> _logger;

        public StockCreatedEvent(IDateTime dateTime, ICurrentUser currentUser, ILogger<StockCreatedEvent> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityCreatedEvent<Stock> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is Created by user {userName} with id {id} at {dateTime}",
               typeof(Stock).FullName,
               notification.Entity.Id,
               _currentUser.UserName,
               _currentUser.Id,
               _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
