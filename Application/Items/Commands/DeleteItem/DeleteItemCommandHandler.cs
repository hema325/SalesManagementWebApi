namespace Application.Items.Commands.DeleteItem
{
    internal class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(request.Id);

            if(item == null)
                throw new NotFoundException("item", new { request.Id });

            item.AddDomainEvent(new EntityDeletedEvent<Item>(item));

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
