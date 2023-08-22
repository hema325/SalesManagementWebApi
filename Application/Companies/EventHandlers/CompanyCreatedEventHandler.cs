using Microsoft.Extensions.Logging;

namespace Application.Companies.EventHandlers
{
    internal class CompanyCreatedEventHandler: INotificationHandler<EntityCreatedEvent<Company>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<CompanyCreatedEventHandler> _logger;

        public CompanyCreatedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<CompanyCreatedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityCreatedEvent<Company> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is Created by user {userName} with id {id} at {dateTime}",
               typeof(Company).FullName,
               notification.Entity.Id,
               _currentUser.UserName,
               _currentUser.Id,
               _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
