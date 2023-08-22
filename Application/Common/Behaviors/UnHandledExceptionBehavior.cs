using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors
{
    internal class UnHandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IDateTime _dateTime;
        private readonly ILogger<UnHandledExceptionBehavior<TRequest, TResponse>> _logger;
        private readonly ICurrentUser _currentUser;

        public UnHandledExceptionBehavior(IDateTime dateTime,
                                          ILogger<UnHandledExceptionBehavior<TRequest, TResponse>> logger,
                                          ICurrentUser currentUser)
        {
            _dateTime = dateTime;
            _logger = logger;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                _logger.LogError("an exception of type {type} was thrown while processing request {requestName} by user {userName} with Id {id} at {dateTime}, message: {message}",
                    ex.GetType().FullName,
                    request.GetType().FullName,
                    _currentUser.UserName,
                    _currentUser.Id,
                    _dateTime.Now,
                    ex.Message);

                throw;
            }
        }
    }
}
