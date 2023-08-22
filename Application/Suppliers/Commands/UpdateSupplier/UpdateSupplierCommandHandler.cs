namespace Application.Suppliers.Commands.UpdateSupplier
{
    internal class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSupplierCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _context.Suppliers.FindAsync(request.Id);

            if (supplier == null)
                throw new NotFoundException("supplier", new { request.Id });
            
            supplier.Name = request.Name;
            supplier.Address = request.Address;
            supplier.PhoneNumbers = request.PhoneNumbers.Select(ph=> new PhoneNumber { Value = ph }).ToList();
            supplier.Notes = request.Notes;

            supplier.AddDomainEvent(new EntityUpdatedEvent<Supplier>(supplier));

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
