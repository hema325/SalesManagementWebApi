using Microsoft.Extensions.Logging;

namespace Application.Categories.EventHandlers
{
    internal class CategoryCreatedEventHandler: INotificationHandler<EntityCreatedEvent<Category>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<CategoryCreatedEventHandler> _logger;

        public CategoryCreatedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<CategoryCreatedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityCreatedEvent<Category> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is Created by user {userName} with id {id} at {dateTime}",
               typeof(Category).FullName,
               notification.Entity.Id,
               _currentUser.UserName,
               _currentUser.Id,
               _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
