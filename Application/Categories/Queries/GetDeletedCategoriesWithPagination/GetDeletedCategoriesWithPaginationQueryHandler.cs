using Application.Categories.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.GetDeletedCategoriesWithPagination
{
    internal class GetDeletedCategoriesWithPaginationQueryHandler : IRequestHandler<GetDeletedCategoriesWithPaginationQuery, PaginatedList<CategoryDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDeletedCategoriesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       
        public async Task<PaginatedList<CategoryDTO>> Handle(GetDeletedCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.IgnoreQueryFilters()
                                             .Where(c => c.DeletedOn != null)
                                             .Include(c => c.Parent)
                                             .PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<CategoryDTO>>(categories);
        }
    }
}
