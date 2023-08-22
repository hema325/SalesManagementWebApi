using Application.Authentication.Common;

namespace Application.Authentication.Commands.Register
{
    public record class RegisterCommand(string UserName, string Password, IEnumerable<Roles> Roles): IRequest<AuthResult>;
}
