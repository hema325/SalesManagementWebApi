namespace Application.Items.Commands.UpdateItem
{
    internal class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(request.Id);

            if(item == null) 
                throw new NotFoundException("item", new { request.Id });

            item.Name = request.Name;
            item.Price = request.Price;
            item.Notes = request.Notes;
            item.IsActive = request.IsActive;
            item.Barcode = request.Barcode;
            item.CompanyId = request.CompanyId;
            item.SupplierId = request.SupplierId;
            item.CategoryId = request.CategoryId;
            item.StockId = request.StockId;
            item.UnitId = request.UnitId;

            item.AddDomainEvent(new EntityUpdatedEvent<Item>(item));

            await _context.SaveChangesAsync();
           
            return Unit.Value;
        }
    }
}
