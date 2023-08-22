using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries.GetItemsReport
{
    internal class GetItemsReportQueryHandler : IRequestHandler<GetItemsReportQuery, List<GetItemsReportQueryResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsReportQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetItemsReportQueryResponse>> Handle(GetItemsReportQuery request, CancellationToken cancellationToken)
        {
            return await _context.Items.Where(i => i.CreatedOn >= request.From && i.CreatedOn <= request.To)
                                 .ProjectTo<GetItemsReportQueryResponse>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
