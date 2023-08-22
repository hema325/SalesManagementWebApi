using Application.Suppliers.Common;

namespace Application.Suppliers.Queries.GetSuppliersWithPagination
{
    internal class GetSuppliersWithPaginationQueryHandler: IRequestHandler<GetSuppliersWithPaginationQuery,PaginatedList<SupplierDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSuppliersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<SupplierDTO>> Handle(GetSuppliersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Suppliers.ProjectTo<SupplierDTO>(_mapper.ConfigurationProvider)
                                           .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
