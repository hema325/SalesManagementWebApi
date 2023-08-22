namespace Application.Invoices.Commands.CreateInvoice
{
    internal class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateInvoiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = new Invoice
            {
                PaidUp = request.PaidUp,
                Discount = request.Discount,
                Type = request.Type,
                BuyerId = request.BuyerId == 0 ? null : request.BuyerId,
                SellerId = request.SellerId == 0 ? null : request.SellerId,
                InvoiceItems = request.InvoiceItems.Select(itm => new InvoiceItem
                {
                    Price = itm.Price,
                    Quantity = itm.Quantity,
                    ItemId = itm.ItemId
                }).ToList()
            };

            invoice.AddDomainEvent(new EntityCreatedEvent<Invoice>(invoice));

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return invoice.Id;
        }
    }
}
