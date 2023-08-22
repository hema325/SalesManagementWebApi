using Microsoft.EntityFrameworkCore;

namespace Application.Items.Commands.CreateItem
{
    public class CreateItemCommandValidator: AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (n, ct) => !await context.Items.AnyAsync(i => i.Name == n)).WithMessage("{PropertyName} is existed");

            RuleFor(c => c.Price)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Barcode)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (b , ct) => !await context.Items.AnyAsync(i => i.Barcode == b)).WithMessage("{PropertyName} is existed");

            RuleFor(c => c.Images)
               .ForEach(builder =>
               {
                   builder.IsImage().WithMessage("{PropertyName} is not a valid image");
               });

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
