using Microsoft.EntityFrameworkCore;

namespace Application.Companies.Queries.GetCompaniesInExcelFile
{
    internal class GetCompaniesInExcelFileQueryHandler : IRequestHandler<GetCompaniesInExcelFileQuery, FileModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileBuilder _fileBuilder;
        private readonly IDateTime _dateTime;

        public GetCompaniesInExcelFileQueryHandler(IApplicationDbContext context, IFileBuilder fileBuilder, IDateTime dateTime)
        {
            _context = context;
            _fileBuilder = fileBuilder;
            _dateTime = dateTime;
        }

        public async Task<FileModel> Handle(GetCompaniesInExcelFileQuery request, CancellationToken cancellationToken)
        {
            var clients = await _context.Companies.ToListAsync();

            var stream = _fileBuilder.BuildExcelFile(clients.Select(c => new
            {
                c.Id,
                c.Name,
                c.Notes,
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
