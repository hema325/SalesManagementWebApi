namespace Application.Companies.Commands.UpdateCompany
{
    internal class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCompanyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies.FindAsync(request.Id);

            if (company == null)
                throw new NotFoundException("company", new { request.Id });

            company.Name = request.Name;
            company.Notes = request.Notes;

            company.AddDomainEvent(new EntityUpdatedEvent<Company>(company));

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
