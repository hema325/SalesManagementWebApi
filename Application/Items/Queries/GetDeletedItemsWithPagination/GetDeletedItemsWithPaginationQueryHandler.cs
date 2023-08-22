using Application.Items.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries.GetDeletedItemsWithPagination
{
    internal class GetDeletedItemsWithPaginationQueryHandler : IRequestHandler<GetDeletedItemsWithPaginationQuery, PaginatedList<ItemDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDeletedItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ItemDTO>> Handle(GetDeletedItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Items.IgnoreQueryFilters()
                                     .Where(i => i.DeletedOn != null)
                                     .ProjectTo<ItemDTO>(_mapper.ConfigurationProvider)
                                     .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
