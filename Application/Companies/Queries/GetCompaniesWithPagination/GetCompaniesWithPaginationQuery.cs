using Application.Companies.Common;

namespace Application.Companies.Queries.GetCompaniesWithPagination
{
    public record GetCompaniesWithPaginationQuery(int PageNumber, int PageSize): IRequest<PaginatedList<CompanyDTO>>;
}
