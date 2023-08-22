namespace Application.Authentication.Commands.RequestJwtToken
{
    public class RequestJwtTokenCommandValidator: AbstractValidator<RequestJwtTokenCommand>
    {
        public RequestJwtTokenCommandValidator()
        {
            RuleFor(c=>c.RefreshToken)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
