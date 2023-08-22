using Microsoft.Extensions.Logging;

namespace Application.Companies.EventHandlers
{
    internal class CompanyUpdatedEventHandler : INotificationHandler<EntityUpdatedEvent<Company>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<CompanyUpdatedEventHandler> _logger;

        public CompanyUpdatedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<CompanyUpdatedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityUpdatedEvent<Company> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is updated by user {userName} with id {id} at {dateTime}",
                typeof(Company).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
