namespace Application.Suppliers.Commands.CreateSupplier
{
    internal class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateSupplierCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = new Supplier
            {
                Name = request.Name,
                Address = request.Address,
                Notes = request.Notes,
                PhoneNumbers = request.PhoneNumbers.Select(ph => new PhoneNumber { Value = ph }).ToList()
            };

            supplier.AddDomainEvent(new EntityCreatedEvent<Supplier>(supplier));

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return supplier.Id;
        }
    }
}
