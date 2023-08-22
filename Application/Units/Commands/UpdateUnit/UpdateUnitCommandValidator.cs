using Microsoft.EntityFrameworkCore;

namespace Application.Units.Commands.UpdateUnit
{
    public class UpdateUnitCommandValidator: AbstractValidator<UpdateUnitCommand>
    {
        public UpdateUnitCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (c, n, ct) => !await context.Units.AnyAsync(u => u.Name == n && u.Id != c.Id)).WithMessage("{PropertyName} is existed");
        }
    }
}
