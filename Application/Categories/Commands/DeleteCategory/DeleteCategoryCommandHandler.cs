﻿namespace Application.Categories.Commands.DeleteCategory
{
    internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);

            if(category == null) 
                throw new NotFoundException("category", new { request.Id });

            category.AddDomainEvent(new EntityDeletedEvent<Category>(category));

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
