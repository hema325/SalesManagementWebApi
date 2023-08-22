namespace Application.Units.Commands.CreateUnit
{
    internal class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateUnitCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = new ItemUnit
            {
                Name = request.Name,
                Notes = request.Notes
            };

            unit.AddDomainEvent(new EntityCreatedEvent<ItemUnit>(unit));

            _context.Units.Add(unit);
            await _context.SaveChangesAsync();

            return unit.Id;
        }
    }
}
