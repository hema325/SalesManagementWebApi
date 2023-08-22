using Application.Companies.Common;

namespace Application.Companies.Queries.GetCompanyById
{
    public record GetCompanyByIdQuery(int Id):IRequest<CompanyDTO>;
}
