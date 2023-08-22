
using Application.Clients.Common;

namespace Application.Clients.Queries.GetClientsWithPagination
{
    internal class GetClientsWithPaginationQueryHandler : IRequestHandler<GetClientsWithPaginationQuery, PaginatedList<ClientDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetClientsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ClientDTO>> Handle(GetClientsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Clients.ProjectTo<ClientDTO>(_mapper.ConfigurationProvider)
                                         .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
