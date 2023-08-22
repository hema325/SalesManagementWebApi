namespace Application.Units.Commands.UpdateUnit
{
    internal class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUnitCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _context.Units.FindAsync(request.Id);

            if(unit == null)
                throw new NotFoundException("unit", new { request.Id });

            unit.Name =request.Name;
            unit.Notes = request.Notes;

            unit.AddDomainEvent(new EntityUpdatedEvent<ItemUnit>(unit));

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
