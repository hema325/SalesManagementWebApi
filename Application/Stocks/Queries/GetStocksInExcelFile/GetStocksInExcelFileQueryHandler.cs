using Microsoft.EntityFrameworkCore;

namespace Application.Stocks.Queries.GetStocksInExcelFile
{
    internal class GetStocksInExcelFileQueryHandler : IRequestHandler<GetStocksInExcelFileQuery,FileModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileBuilder _fileBuilder;
        private readonly IDateTime _dateTime;

        public GetStocksInExcelFileQueryHandler(IApplicationDbContext context, IFileBuilder fileBuilder, IDateTime dateTime)
        {
            _context = context;
            _fileBuilder = fileBuilder;
            _dateTime = dateTime;
        }

        public async Task<FileModel> Handle(GetStocksInExcelFileQuery request, CancellationToken cancellationToken)
        {
            var stocks = await _context.Stocks.ToListAsync();

            var stream  = _fileBuilder.BuildExcelFile(stocks.Select(s => new
            {
                s.Id,
                s.Name,
                s.Address,
                s.Notes,
                s.CreatedOn
            }));

            return new FileModel
            {
                Stream = stream,
                Name = $"{_dateTime.Now}-Stocks",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
    }
}
