using Application.Companies.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Companies.Queries.GetDeletedCompaniesWithPagination
{
    internal class GetDeletedCompaniesWithPaginationQueryHandler : IRequestHandler<GetDeletedCompaniesWithPaginationQuery, PaginatedList<CompanyDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDeletedCompaniesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<CompanyDTO>> Handle(GetDeletedCompaniesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Companies.IgnoreQueryFilters()
                                     .Where(c => c.DeletedOn != null)
                                     .ProjectTo<CompanyDTO>(_mapper.ConfigurationProvider)
                                     .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
