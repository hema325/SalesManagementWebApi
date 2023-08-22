namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator(IIdentityService identityService)
        {
            RuleFor(c => c.UserName)
                .MustAsync(async (n, ct) => !await identityService.IsUserNameExsitsAsync(n)).WithMessage("{PropertyName} is already Existed")
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .Must(IsValidPassword).WithMessage("{PropertyName} must contains small, capital, digits and special characters")
                .Length(6, 24).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}");

            RuleFor(c => c.Roles)
                .ForEach(roleBuilder =>
                {
                    roleBuilder.IsInEnum().WithMessage("{PropertyName} is not valid");
                });
        }

        private bool IsValidPassword(string password)
        {
            var specialCharacters = new[] { '!', '@', '#', '$', '%', '^', '&', '*', ' ', '.', '?', '"', '\'', '-', '+', '=', '<', '>', '/', '\\' };

            return password.Any(c => char.IsUpper(c))
                && password.Any(c => char.IsLower(c))
                && password.Any(c => char.IsDigit(c))
                && password.Any(c => specialCharacters.Any(sc => sc == c));
        }
    }
}
