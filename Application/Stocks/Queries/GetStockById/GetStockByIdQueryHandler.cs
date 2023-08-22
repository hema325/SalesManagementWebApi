using Application.Stocks.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Stocks.Queries.GetStockById
{
    internal class GetStockByIdQueryHandler : IRequestHandler<GetStockByIdQuery, StockDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStockByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StockDTO> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Stocks.ProjectTo<StockDTO>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync(s => s.Id == request.Id);
        }
    }
}
