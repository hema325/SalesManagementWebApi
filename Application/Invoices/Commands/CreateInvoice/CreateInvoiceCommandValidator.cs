using Microsoft.EntityFrameworkCore;

namespace Application.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.PaidUp)
                .PrecisionScale(9, 2, false);

            RuleFor(c => c.Discount)
                .PrecisionScale(5, 2, false);

            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .IsInEnum().WithMessage("{PropertyName} is not valid");

            RuleFor(c => c.BuyerId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MustAsync(async (i, ct) => await context.Clients.AnyAsync(c => c.Id == i)).WithMessage("{PropertyName} is not valid")
                .When((c, i) => c.Type == Domain.Enums.Invoices.Selling || c.Type == Domain.Enums.Invoices.Returning);

            RuleFor(c => c.SellerId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MustAsync(async (i, ct) => await context.Suppliers.AnyAsync(s => s.Id == i)).WithMessage("{PropertyName} is not valid")
                .When((c, i) => c.Type == Domain.Enums.Invoices.Buying || c.Type == Domain.Enums.Invoices.Returning);

            RuleFor(c => c.InvoiceItems)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .ForEach(builder =>
                {
                    builder.NotEmpty().WithMessage("{PropertyName} is required");
                    builder.SetValidator(new CreateInvoiceItemCommandValidator(context));
                });
        }
    }

    internal class CreateInvoiceItemCommandValidator: AbstractValidator<CreateInvoiceItemCommand>
    {
        public CreateInvoiceItemCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c=>c.Price)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .PrecisionScale(9, 2, false);

            RuleFor(c => c.Quantity)
               .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(c => c.ItemId)
               .Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .MustAsync(async (i, ct) => await context.Items.AnyAsync(itm => itm.Id == i)).WithMessage("{PropertyName} is not valid");
        }
    }
}
