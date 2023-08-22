using Microsoft.EntityFrameworkCore;

namespace Application.Invoices.Queries.GetDeletedInvoicesInExcelFile
{
    internal class GetInvoicesInExcelFileQueryHandler : IRequestHandler<GetInvoicesInExcelFileQuery, FileModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileBuilder _fileBuilder;
        private readonly IDateTime _dateTime;

        public GetInvoicesInExcelFileQueryHandler(IApplicationDbContext context, IFileBuilder fileBuilder, IDateTime dateTime)
        {
            _context = context;
            _fileBuilder = fileBuilder;
            _dateTime = dateTime;
        }

        public async Task<FileModel> Handle(GetInvoicesInExcelFileQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _context.Invoices.Include(i=>i.InvoiceItems).ToListAsync();

            var stream = _fileBuilder.BuildExcelFile(invoices.Select(c => new
            {
                c.Id,
                c.PaidUp,
                c.Discount,
                Type = c.Type.ToString(),
                c.BuyerId,
                c.SellerId,
                ItemsId = string.Join(",", c.InvoiceItems.Select(it => it.ItemId)),
                c.CreatedOn
            }));

            return new FileModel
            {
                Stream = stream,
                Name = $"{_dateTime.Now}-Companies",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
    }
}
