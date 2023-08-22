using Application.Companies.Common;

namespace Application.Companies.Queries.GetDeletedCompaniesWithPagination
{
    public record GetDeletedCompaniesWithPaginationQuery(int PageSize, int PageNumber): IRequest<PaginatedList<CompanyDTO>>;
}
