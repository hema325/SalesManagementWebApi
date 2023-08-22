using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.GetCategoriesInExcelFile
{
    internal class GetCategoriesInExcelFileQueryHandler : IRequestHandler<GetCategoriesInExcelFileQuery, FileModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileBuilder _fileBuilder;
        private readonly IDateTime _dateTime;

        public GetCategoriesInExcelFileQueryHandler(IApplicationDbContext context, IFileBuilder fileBuilder, IDateTime dateTime)
        {
            _context = context;
            _fileBuilder = fileBuilder;
            _dateTime = dateTime;
        }

        public async Task<FileModel> Handle(GetCategoriesInExcelFileQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.ToListAsync();
            var stream = _fileBuilder.BuildExcelFile(categories.Select(c => new
            {
                c.Id,
                c.Name,
                c.Notes,
                c.CreatedOn
            }));

            return new FileModel
            {
                Stream = stream,
                Name = $"{_dateTime.Now}-Categories",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
    }
}
