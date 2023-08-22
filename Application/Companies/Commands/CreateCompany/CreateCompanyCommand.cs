namespace Application.Companies.Commands.CreateCompany
{
    public record CreateCompanyCommand(string Name, string Notes): IRequest<int>;
}
