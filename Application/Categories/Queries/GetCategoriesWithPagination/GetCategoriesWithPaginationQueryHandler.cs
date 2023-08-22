using Application.Categories.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.GetCategoriesWithPagination
{
    internal class GetCategoriesWithPaginationQueryHandler : IRequestHandler<GetCategoriesWithPaginationQuery, PaginatedList<CategoryDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CategoryDTO>> Handle(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<CategoryDTO>>(categories);
        }
    }
}
