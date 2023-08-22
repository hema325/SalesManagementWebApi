using Application.Items.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries.GetItemById
{
    internal class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ItemDTO> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Items.ProjectTo<ItemDTO>(_mapper.ConfigurationProvider)
                                            .FirstOrDefaultAsync(c => c.Id == request.Id);
        }
    }
}
