namespace Application.Units.Commands.DeleteUnit
{
    internal class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteUnitCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _context.Units.FindAsync(request.Id);

            if (unit == null)
                throw new NotFoundException("unit", new { request.Id });

            unit.AddDomainEvent(new EntityDeletedEvent<ItemUnit>(unit));

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
