namespace Application.Stocks.Commands.UpdateStock
{
    internal class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStockCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _context.Stocks.FindAsync(request.Id);

            if (stock == null)
                throw new NotFoundException("stock", new { request.Id });

            stock.Name = request.Name;
            stock.Address = request.Address;
            stock.Notes = request.Notes;

            stock.AddDomainEvent(new EntityUpdatedEvent<Stock>(stock));

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
