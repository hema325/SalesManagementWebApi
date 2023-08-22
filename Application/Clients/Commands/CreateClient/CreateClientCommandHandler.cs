using Domain.Common.Events;

namespace Application.Clients.Commands.CreateClient
{
    internal class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public CreateClientCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var images = new List<Image>();

            if (request.Images != null)
                images = (await Task.WhenAll(request.Images.Select(i => _fileStorage.SaveAsync(i)))).Select(p => new Image { Path = p }).ToList();

            var client = new Client
            {
                Name = request.Name,
                Gender =  Enum.Parse<Gender>(request.Gender,true),
                DateOfBirth = request.DateOfBirth,
                PhoneNumbers = request.PhoneNumbers.Select(ph => new PhoneNumber { Value = ph }).ToList(),
                Address = request.Address,
                Images = images
            };

            client.AddDomainEvent(new EntityCreatedEvent<Client>(client));

            _context.Clients.Add(client);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                images.ForEach(i => _fileStorage.Delete(i.Path));
                throw;
            }

            return client.Id;
        }
    }
}
