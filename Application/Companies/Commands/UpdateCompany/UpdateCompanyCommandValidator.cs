using Microsoft.EntityFrameworkCore;

namespace Application.Companies.Commands.UpdateCompany
{
    internal class UpdateCompanyCommandValidator: AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (c, n, ct) => !await context.Companies.AnyAsync(cmp => cmp.Name == n && cmp.Id != c.Id)).WithMessage("{PropertyName} is existed");
        }
    }
}
