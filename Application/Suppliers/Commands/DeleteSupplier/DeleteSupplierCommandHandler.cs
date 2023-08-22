namespace Application.Suppliers.Commands.DeleteSupplier
{
    internal class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSupplierCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.FindAsync(request.Id);

            if(supplier == null) 
                throw new NotFoundException("supplier", new { request.Id });

            supplier.AddDomainEvent(new EntityDeletedEvent<Supplier>(supplier));

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
