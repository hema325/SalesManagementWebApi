using Application.Suppliers.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Suppliers.Queries.GetSupplierById
{
    internal class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, SupplierDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSupplierByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<SupplierDTO> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Suppliers.ProjectTo<SupplierDTO>(_mapper.ConfigurationProvider)
                                           .FirstOrDefaultAsync(s=>s.Id == request.Id);
        }
    }
}
