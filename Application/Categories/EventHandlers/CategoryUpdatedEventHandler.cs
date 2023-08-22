using Microsoft.Extensions.Logging;

namespace Application.Categories.EventHandlers
{
    internal class CategoryUpdatedEventHandler: INotificationHandler<EntityUpdatedEvent<Category>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<CategoryUpdatedEventHandler> _logger;

        public CategoryUpdatedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<CategoryUpdatedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityUpdatedEvent<Category> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is updated by user {userName} with id {id} at {dateTime}",
                typeof(Category).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }

    }
}
