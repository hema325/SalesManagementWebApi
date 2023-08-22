using Application.Invoices.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Invoices.Queries.GetInvoiceById
{
    internal class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetInvoiceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InvoiceDTO> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Invoices.ProjectTo<InvoiceDTO>(_mapper.ConfigurationProvider)
                                          .FirstOrDefaultAsync(c => c.Id == request.Id);
        }
    }
}
