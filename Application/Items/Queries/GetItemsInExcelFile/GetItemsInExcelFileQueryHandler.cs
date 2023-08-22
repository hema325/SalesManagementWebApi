using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries.GetItemsInExcelFile
{
    internal class GetItemsInExcelFileQueryHandler : IRequestHandler<GetItemsInExcelFileQuery, FileModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileBuilder _fileBuilder;
        private readonly IDateTime _dateTime;

        public GetItemsInExcelFileQueryHandler(IApplicationDbContext context, IFileBuilder fileBuilder, IDateTime dateTime)
        {
            _context = context;
            _fileBuilder = fileBuilder;
            _dateTime = dateTime;
        }


        public async Task<FileModel> Handle(GetItemsInExcelFileQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Items.ToListAsync();

            var stream = _fileBuilder.BuildExcelFile(items.Select(c => new
            {
                c.Id,
                c.Name,
                c.Price,
                c.Notes,
                c.IsActive,
                c.Barcode,
                Images = string.Join(",",c.Images.Select(i=>i.Path)),
                c.CompanyId,
                c.SupplierId,
                c.CategoryId,
                c.StockId,
                c.CreatedOn
            }));

            return new FileModel
            {
                Stream = stream,
                Name = $"{_dateTime.Now}-Items",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
    }
}
