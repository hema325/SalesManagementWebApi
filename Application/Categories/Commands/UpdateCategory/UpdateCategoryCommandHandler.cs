namespace Application.Categories.Commands.UpdateCategory
{
    internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);

            if (category == null)
                throw new NotFoundException("category", new { request.Id });

            category.Name = request.Name;
            category.Notes = request.Notes;
            category.ParentId = request.ParentId == 0 ? null : request.ParentId;

            category.AddDomainEvent(new EntityUpdatedEvent<Category>(category));

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
