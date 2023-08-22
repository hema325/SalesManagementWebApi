using Microsoft.Extensions.Logging;

namespace Application.Companies.EventHandlers
{
    internal class CompanyDeletedEventHandler: INotificationHandler<EntityDeletedEvent<Company>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<CompanyDeletedEventHandler> _logger;

        public CompanyDeletedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<CompanyDeletedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityDeletedEvent<Company> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is deleted by user {userName} with id {id} at {dateTime}",
                typeof(Company).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
