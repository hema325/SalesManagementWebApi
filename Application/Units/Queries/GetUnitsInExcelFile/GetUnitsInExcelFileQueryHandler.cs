using Microsoft.EntityFrameworkCore;

namespace Application.Units.Queries.GetUnitsInExcelFile
{
    internal class GetUnitsInExcelFileQueryHandler : IRequestHandler<GetUnitsInExcelFileQuery, FileModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileBuilder _fileBuilder;
        private readonly IDateTime _dateTime;

        public GetUnitsInExcelFileQueryHandler(IApplicationDbContext context, IFileBuilder fileBuilder, IDateTime dateTime)
        {
            _context = context;
            _fileBuilder = fileBuilder;
            _dateTime = dateTime;
        }

        public async Task<FileModel> Handle(GetUnitsInExcelFileQuery request, CancellationToken cancellationToken)
        {
            var units = await _context.Units.ToListAsync();

            var stream = _fileBuilder.BuildExcelFile(units.Select(u => new
            {
                u.Id,
                u.Name,
                u.Notes,
                u.CreatedOn
            }));

            return new FileModel
            {
                Stream = stream,
                Name = $"{_dateTime.Now}-Units",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
    }
}
