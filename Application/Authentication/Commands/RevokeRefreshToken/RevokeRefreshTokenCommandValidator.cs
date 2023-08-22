namespace Application.Authentication.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandValidator: AbstractValidator<RevokeRefreshTokenCommand>
    {
        public RevokeRefreshTokenCommandValidator()
        {
            RuleFor(c => c.RefreshToken)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
