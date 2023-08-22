namespace Application.Categories.Commands.CreateCategory
{
    internal class CreateCategoryCommandHandler: IRequestHandler<CreateCategoryCommand,int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                Notes = request.Notes,
                ParentId = request.ParentId == 0 ? null: request.ParentId
            };

            category.AddDomainEvent(new EntityCreatedEvent<Category>(category));

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category.Id;
        }
    }
}
