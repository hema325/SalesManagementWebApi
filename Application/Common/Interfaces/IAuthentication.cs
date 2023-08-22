using Application.Authentication.Common;
using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface IAuthentication
    {
        Task<AuthResult> AuthenticateAsync(string username, string password);
        Task<AuthResult> RegisterAsync(string userName, string password, IEnumerable<Roles> roles);
        Task<AuthResult> RequestJwtTokenAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}
