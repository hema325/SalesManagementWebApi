using Application.Units.Common;

namespace Application.Units.Queries.GetUnitsWithPagination
{
    internal class GetUnitsWithPaginationQueryHandler: IRequestHandler<GetUnitsWithPaginationQuery,PaginatedList<UnitDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUnitsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UnitDTO>> Handle(GetUnitsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Units.ProjectTo<UnitDTO>(_mapper.ConfigurationProvider)
                                       .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
