

namespace Application.Stocks.Commands.DeleteStock
{
    internal class DeleteStockCommandHandler : IRequestHandler<DeleteStockCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteStockCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await _context.Stocks.FindAsync(request.Id);

            if (stock == null)
                throw new NotFoundException("stock", new {request.Id });

            stock.AddDomainEvent(new EntityDeletedEvent<Stock>(stock));

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
