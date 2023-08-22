using Application.Units.Common;

namespace Application.Units.Queries.GetUnitsWithPagination
{
    public record GetUnitsWithPaginationQuery(int PageNumber, int PageSize): IRequest<PaginatedList<UnitDTO>>;
}
