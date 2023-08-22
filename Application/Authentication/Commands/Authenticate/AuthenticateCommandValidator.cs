using Application.Common.Extensions;

namespace Application.Authentication.Commands.Authenticate
{
    public class AuthenticateCommandValidator: AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
