namespace Application.Companies.Commands.UpdateCompany
{
    public record UpdateCompanyCommand(int Id, string Name, string Notes):IRequest;
}
