using Application.Units.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Units.Queries.GetUnitById
{
    internal class GetUnitByIdQueryHandler : IRequestHandler<GetUnitByIdQuery, UnitDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUnitByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UnitDTO> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Units.ProjectTo<UnitDTO>(_mapper.ConfigurationProvider)
                                       .FirstOrDefaultAsync(u => u.Id == request.Id);
        }
    }
}
