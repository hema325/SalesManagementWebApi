using Microsoft.EntityFrameworkCore;

namespace Application.Invoices.Commands.UpdateInvoice
{
    internal class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateInvoiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _context.Invoices.Include(i => i.InvoiceItems).FirstOrDefaultAsync(i => i.Id == request.Id);

            if(invoice == null) 
                throw new NotFoundException("invoice", new {request.Id});

            invoice.PaidUp = request.PaidUp;
            invoice.Discount = request.Discount;
            invoice.Type = request.Type;
            invoice.BuyerId = request.BuyerId;
            invoice.SellerId = request.SellerId;
            invoice.InvoiceItems = request.InvoiceItems?.Select(i => new InvoiceItem
            {
                Price = i.Price,
                Quantity = i.Quantity,
                ItemId = i.ItemId
            }).ToList();

            invoice.AddDomainEvent(new EntityUpdatedEvent<Invoice>(invoice));

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
