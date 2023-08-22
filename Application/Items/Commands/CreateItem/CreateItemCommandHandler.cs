namespace Application.Items.Commands.CreateItem
{
    internal class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public CreateItemCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var images = new List<Image>();

            if (request.Images != null)
                images = (await Task.WhenAll(request.Images.Select(i => _fileStorage.SaveAsync(i)))).Select(p => new Image { Path = p }).ToList();

            var item = new Item
            {
                Name = request.Name,
                Price = request.Price,
                Notes = request.Notes,
                IsActive = request.IsActive,
                Barcode = request.Barcode,
                Images = images,
                CompanyId = request.CompanyId,
                SupplierId = request.SupplierId,
                CategoryId = request.CategoryId,
                StockId = request.StockId,
                UnitId = request.UnitId
            };

            item.AddDomainEvent(new EntityCreatedEvent<Item>(item));

            _context.Items.Add(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                images.ForEach(i => _fileStorage.Delete(i.Path));
                throw;
            }

            return item.Id;
        }
    }
}
