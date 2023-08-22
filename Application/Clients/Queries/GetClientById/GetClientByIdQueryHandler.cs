using Application.Clients.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Clients.Queries.GetClientById
{
    internal class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientDTO> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Clients.ProjectTo<ClientDTO>(_mapper.ConfigurationProvider)
                                         .FirstOrDefaultAsync(c => c.Id == request.Id);
        }
    }
}
