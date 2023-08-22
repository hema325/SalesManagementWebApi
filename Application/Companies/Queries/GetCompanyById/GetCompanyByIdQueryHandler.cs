using Application.Companies.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Companies.Queries.GetCompanyById
{
    internal class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompanyByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompanyDTO> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Companies.ProjectTo<CompanyDTO>(_mapper.ConfigurationProvider)
                                           .FirstOrDefaultAsync(c => c.Id == request.Id);
        }
    }
}
