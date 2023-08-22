using Domain.Entities;

namespace Application.Invoices.Commands.DeleteInvoice
{
    internal class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteInvoiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _context.Invoices.FindAsync(request.Id);

            if (invoice == null)
                throw new NotFoundException("invoice", new { request.Id });

            invoice.AddDomainEvent(new EntityDeletedEvent<Invoice>(invoice));

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
