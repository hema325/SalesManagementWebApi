using Application.Invoices.Common;

namespace Application.Invoices.Queries.GetInvoicesWithPagination
{
    internal class GetInvoicesWithPaginationQueryHandler : IRequestHandler<GetInvoicesWithPaginationQuery, PaginatedList<InvoiceDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetInvoicesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<InvoiceDTO>> Handle(GetInvoicesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Invoices.ProjectTo<InvoiceDTO>(_mapper.ConfigurationProvider)
                                           .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
