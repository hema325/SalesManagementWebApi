using Microsoft.EntityFrameworkCore;

namespace Application.Clients.Queries.GetClientsInExcelFile
{
    internal class GetClientsInExcelFileQueryHandler : IRequestHandler<GetClientsInExcelFileQuery, FileModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileBuilder _fileBuilder;
        private readonly IDateTime _dateTime;

        public GetClientsInExcelFileQueryHandler(IApplicationDbContext context, IFileBuilder fileBuilder, IDateTime dateTime)
        {
            _context = context;
            _fileBuilder = fileBuilder;
            _dateTime = dateTime;
        }

        public async Task<FileModel> Handle(GetClientsInExcelFileQuery request, CancellationToken cancellationToken)
        {
            var clients = await _context.Clients.ToListAsync();
            var stream = _fileBuilder.BuildExcelFile(clients.Select(c => new
            {
                c.Id,
                c.Name,
                c.Gender,
                c.DateOfBirth,
                c.Address,
                PhoneNumbers = string.Join(",",c.PhoneNumbers.Select(ph => ph.Value)),
                Images = string.Join(",",c.Images.Select(i => i.Path)),
                c.CreatedOn
            }));

            return new FileModel
            {
                Stream = stream,
                Name = $"{_dateTime.Now}-Clients",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
    }
}
