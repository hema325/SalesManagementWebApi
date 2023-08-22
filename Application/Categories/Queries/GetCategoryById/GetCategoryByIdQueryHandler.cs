using Application.Categories.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.GetCategoryById
{
    internal class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Include(c => c.Parent)
                                                    .FirstOrDefaultAsync(c => c.Id == request.Id);

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
