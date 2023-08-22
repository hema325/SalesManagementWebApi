using Application.Stocks.Common;

namespace Application.Stocks.Queries.GetStocksWithPagination
{
    internal class GetStocksWithPaginationQueryHandler : IRequestHandler<GetStocksWithPaginationQuery, PaginatedList<StockDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStocksWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<StockDTO>> Handle(GetStocksWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Stocks.ProjectTo<StockDTO>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
