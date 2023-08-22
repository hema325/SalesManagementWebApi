using Microsoft.Extensions.Logging;

namespace Application.Categories.EventHandlers
{
    internal class CategoryDeletedEventHandler: INotificationHandler<EntityDeletedEvent<Category>>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<CategoryDeletedEventHandler> _logger;

        public CategoryDeletedEventHandler(IDateTime dateTime, ICurrentUser currentUser, ILogger<CategoryDeletedEventHandler> logger)
        {
            _dateTime = dateTime;
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityDeletedEvent<Category> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {entityId} is deleted by user {userName} with id {id} at {dateTime}",
                typeof(Category).FullName,
                notification.Entity.Id,
                _currentUser.UserName,
                _currentUser.Id,
                _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}
