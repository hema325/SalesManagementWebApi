using Domain.Common.Events;

namespace Application.Companies.Commands.DeleteCompany
{
    internal class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCompanyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies.FindAsync(request.Id);

            if (company == null)
                throw new NotFoundException("company", new { request.Id });

            company.AddDomainEvent(new EntityDeletedEvent<Company>(company));

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
