
using Domain.Common.Events;

namespace Application.Clients.Commands.UpdateClient
{
    internal class UpdateClientCommandHandler: IRequestHandler<UpdateClientCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateClientCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FindAsync(request.Id);

            if (client == null)
                throw new NotFoundException("client", new { request.Id });

            client.Name = request.Name;
            client.Gender = request.Gender;
            client.DateOfBirth = request.DateOfBirth;
            client.PhoneNumbers = request.PhoneNumbers.Select(ph => new PhoneNumber { Value = ph }).ToList();
            client.Address = request.Address;

            client.AddDomainEvent(new EntityUpdatedEvent<Client>(client));

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
