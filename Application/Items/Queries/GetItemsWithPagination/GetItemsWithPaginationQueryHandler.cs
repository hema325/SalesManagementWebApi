using Application.Items.Common;

namespace Application.Items.Queries.GetItemsWithPagination
{
    internal class GetItemsWithPaginationQueryHandler : IRequestHandler<GetItemsWithPaginationQuery, PaginatedList<ItemDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ItemDTO>> Handle(GetItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Items.ProjectTo<ItemDTO>(_mapper.ConfigurationProvider)
                                       .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
