using Application.Authentication.Common;

namespace Application.Authentication.Commands.RequestJwtToken
{
    internal class RequestJwtTokenCommandHandler : IRequestHandler<RequestJwtTokenCommand,AuthResult>
    {
        private readonly IAuthentication _auth;

        public RequestJwtTokenCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        async Task<AuthResult> IRequestHandler<RequestJwtTokenCommand, AuthResult>.Handle(RequestJwtTokenCommand request, CancellationToken cancellationToken)
        {
            return await _auth.RequestJwtTokenAsync(request.RefreshToken);
        }
    }
}
