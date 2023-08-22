using Application.Clients.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Clients.Queries.GetDeletedClientsWithPagination
{
    internal class GetDeletedClientsWithPaginationQueryHandler : IRequestHandler<GetDeletedClientsWithPaginationQuery, PaginatedList<ClientDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDeletedClientsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ClientDTO>> Handle(GetDeletedClientsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Clients.IgnoreQueryFilters()
                                         .Where(c => c.DeletedOn != null)
                                         .ProjectTo<ClientDTO>(_mapper.ConfigurationProvider)
                                         .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
