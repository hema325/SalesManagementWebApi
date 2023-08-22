using Microsoft.EntityFrameworkCore;

namespace Application.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandHandlerValidator: AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierCommandHandlerValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(256).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength}")
                .MustAsync(async (c, n, ct) => !await context.Suppliers.AnyAsync(s => s.Name == n && s.Id != c.Id)).WithMessage("{PropertyName} is existed");

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.PhoneNumbers)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .ForEach(builder =>
                {
                    builder.NotEmpty().WithMessage("{PropertyName} is required");
                    builder.MaximumLength(24).WithMessage("{PropertyName} length must be less than {MaxLength}");
                    builder.Must(ph => ph.All(c => char.IsDigit(c))).WithMessage("{PropertyName} must contains numbers only");
                });
        }
    }
}
