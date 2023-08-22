using Microsoft.EntityFrameworkCore;

namespace Application.Stocks.Commands.UpdateStock
{
    public class UpdateStockCommandValidator: AbstractValidator<UpdateStockCommand>
    {
        public UpdateStockCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (c, n, ct) => !await context.Stocks.AnyAsync(s => s.Name == n && s.Id != c.Id)).WithMessage("{PropertyName} is existed");

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
