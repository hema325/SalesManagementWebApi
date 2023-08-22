using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors
{
    internal class LogginBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger<LogginBehavior<TRequest>> _logger;
        private readonly ICurrentUser _currentUser;
        private readonly IDateTime _dateTime;

        public LogginBehavior(ILogger<LogginBehavior<TRequest>> logger, ICurrentUser currentUser, IDateTime dateTime)
        {
            _logger = logger;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("request with name {requestName} has done by user {userName} with id {id} at {dateTime}",
                                   request.GetType().FullName,
                                   _currentUser.UserName,
                                   _currentUser.Id,
                                   _dateTime.Now);

            return Task.CompletedTask;
        }
    }
}

