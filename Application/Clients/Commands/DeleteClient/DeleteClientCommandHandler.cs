
namespace Application.Clients.Commands.DeleteClient
{
    internal class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteClientCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FindAsync(request.Id);

            if (client == null)
                throw new NotFoundException("client", new { request.Id });

            client.AddDomainEvent(new EntityDeletedEvent<Client>(client));

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
