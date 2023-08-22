using Microsoft.EntityFrameworkCore;

namespace Application.Suppliers.Queries.GetSuppliersInExcelFile
{
    internal class GetSuppliersInExcelFileQueryHandler : IRequestHandler<GetSuppliersInExcelFileQuery, FileModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileBuilder _fileBuilder;
        private readonly IDateTime _dateTime;

        public GetSuppliersInExcelFileQueryHandler(IApplicationDbContext context, IFileBuilder fileBuilder, IDateTime dateTime)
        {
            _context = context;
            _fileBuilder = fileBuilder;
            _dateTime = dateTime;
        }
        public async Task<FileModel> Handle(GetSuppliersInExcelFileQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _context.Suppliers.ToListAsync();

            var stream = _fileBuilder.BuildExcelFile(suppliers.Select(s => new
            {
                s.Name,
                s.Address,
                s.Notes,
                s.CreatedOn,
                PhoneNumbers = string.Join(",",s.PhoneNumbers.Select(ph=>ph.Value))
            }));

            return new FileModel
            {
                Stream = stream,
                Name = $"{_dateTime.Now}-Suppliers",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
    }
}
