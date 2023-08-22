using Microsoft.EntityFrameworkCore;

namespace Application.Units.Commands.CreateUnit
{
    public class CreateUnitCommandValidator: AbstractValidator<CreateUnitCommand>
    {
        public CreateUnitCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (n, ct) => !await context.Units.AnyAsync(u => u.Name == n)).WithMessage("{PropertyName} is existed");
        }
    }
}
