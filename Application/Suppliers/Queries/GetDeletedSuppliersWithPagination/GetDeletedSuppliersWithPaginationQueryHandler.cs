using Application.Suppliers.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Suppliers.Queries.GetDeletedSuppliersWithPagination
{
    internal class GetDeletedSuppliersWithPaginationQueryHandler : IRequestHandler<GetDeletedSuppliersWithPaginationQuery, PaginatedList<SupplierDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDeletedSuppliersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<PaginatedList<SupplierDTO>> IRequestHandler<GetDeletedSuppliersWithPaginationQuery, PaginatedList<SupplierDTO>>.Handle(GetDeletedSuppliersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Suppliers.IgnoreQueryFilters()
                                           .Where(s => s.DeletedOn != null)
                                           .ProjectTo<SupplierDTO>(_mapper.ConfigurationProvider)
                                           .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
