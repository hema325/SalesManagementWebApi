using Application.Companies.Common;

namespace Application.Companies.Queries.GetCompaniesWithPagination
{
    internal class GetCompaniesWithPaginationQueryHandler : IRequestHandler<GetCompaniesWithPaginationQuery, PaginatedList<CompanyDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompaniesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CompanyDTO>> Handle(GetCompaniesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Companies.ProjectTo<CompanyDTO>(_mapper.ConfigurationProvider)
                                         .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
