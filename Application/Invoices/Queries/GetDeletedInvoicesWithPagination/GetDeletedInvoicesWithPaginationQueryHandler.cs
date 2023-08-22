using Application.Invoices.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Invoices.Queries.GetDeletedInvoicesWithPagination
{
    internal class GetDeletedInvoicesWithPaginationQueryHandler : IRequestHandler<GetDeletedInvoicesWithPaginationQuery, PaginatedList<InvoiceDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDeletedInvoicesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<InvoiceDTO>> Handle(GetDeletedInvoicesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Invoices.IgnoreQueryFilters()
                                    .Where(c => c.DeletedOn != null)
                                    .ProjectTo<InvoiceDTO>(_mapper.ConfigurationProvider)
                                    .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
