using Application.Stocks.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Stocks.Queries.GetDeletedStocksWithPagination
{
    internal class GetDeletedStocksWithPaginationQueryHandler : IRequestHandler<GetDeletedStocksWithPaginationQuery, PaginatedList<StockDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDeletedStocksWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<StockDTO>> Handle(GetDeletedStocksWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Stocks.IgnoreQueryFilters()
                .Where(s => s.DeletedOn != null)
                .ProjectTo<StockDTO>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
