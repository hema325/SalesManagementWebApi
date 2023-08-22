using Application.Units.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Units.Queries.GetDeletedUnitsWithPagination
{
    internal class GetDeletedUnitsWithPaginationQueryHandler : IRequestHandler<GetDeletedUnitsWithPaginationQuery, PaginatedList<UnitDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDeletedUnitsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UnitDTO>> Handle(GetDeletedUnitsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Units.IgnoreQueryFilters()
                                       .Where(u => u.DeletedOn != null)
                                       .ProjectTo<UnitDTO>(_mapper.ConfigurationProvider)
                                       .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
