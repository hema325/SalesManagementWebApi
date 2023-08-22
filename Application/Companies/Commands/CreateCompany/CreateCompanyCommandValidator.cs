using Microsoft.EntityFrameworkCore;

namespace Application.Companies.Commands.CreateCompany
{
    internal class CreateCompanyCommandValidator:AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c=>c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (n, ct) => !await context.Companies.AnyAsync(s => s.Name == n)).WithMessage("{PropertyName} is existed");
        }
    }
}
