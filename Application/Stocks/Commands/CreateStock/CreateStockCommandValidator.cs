using Microsoft.EntityFrameworkCore;

namespace Application.Stocks.Commands.CreateStock
{
    public class CreateStockCommandValidator: AbstractValidator<CreateStockCommand>
    {
        public CreateStockCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (n, ct) => !await context.Stocks.AnyAsync(s => s.Name == n)).WithMessage("{PropertyName} is existed");

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
