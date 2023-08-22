using Microsoft.EntityFrameworkCore;

namespace Application.Items.Commands.UpdateItem
{
    public class UpdateItemCommandValidator: AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Id)
               .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (c, n, ct) => !await context.Items.AnyAsync(i => i.Name == n && i.Id != c.Id)).WithMessage("{PropertyName} is existed");

            RuleFor(c => c.Price)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Barcode)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (c, b, ct) => !await context.Items.AnyAsync(i => i.Barcode == b && i.Id != c.Id)).WithMessage("{PropertyName} is existed");

            RuleFor(c => c.CompanyId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MustAsync(async (i, ct) => await context.Companies.AnyAsync(c => c.Id == i)).WithMessage("{PropertyName} is not valid");

            RuleFor(c => c.SupplierId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MustAsync(async (i, ct) => await context.Suppliers.AnyAsync(s => s.Id == i)).WithMessage("{PropertyName} is not valid");

            RuleFor(c => c.CategoryId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MustAsync(async (i, ct) => await context.Categories.AnyAsync(c => c.Id == i)).WithMessage("{PropertyName} is not valid");

            RuleFor(c => c.StockId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MustAsync(async (i, ct) => await context.Stocks.AnyAsync(s => s.Id == i)).WithMessage("{PropertyName} is not valid");

            RuleFor(c => c.UnitId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MustAsync(async (i, ct) => await context.Units.AnyAsync(u => u.Id == i)).WithMessage("{PropertyName} is not valid");
        }
    }
}
