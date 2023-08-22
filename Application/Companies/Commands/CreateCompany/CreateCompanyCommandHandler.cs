using Domain.Common.Events;

namespace Application.Companies.Commands.CreateCompany
{
    internal class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand,int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCompanyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Company
            {
                Name = request.Name,
                Notes = request.Notes
            };

            company.AddDomainEvent(new EntityCreatedEvent<Company>(company));

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return company.Id;
        }
    }
}
