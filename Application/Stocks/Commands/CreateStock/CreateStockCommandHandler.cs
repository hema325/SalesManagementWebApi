namespace Application.Stocks.Commands.CreateStock
{
    internal class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateStockCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var stock = new Stock
            {
                Name = request.Name,
                Address = request.Address,
                Notes = request.Notes
            };

            stock.AddDomainEvent(new EntityCreatedEvent<Stock>(stock));

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return stock.Id;
        }
    }
}
