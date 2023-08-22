using Application.Units.Common;

namespace Application.Units.Queries.GetDeletedUnitsWithPagination
{
    public record GetDeletedUnitsWithPaginationQuery(int PageNumber, int PageSize):IRequest<PaginatedList<UnitDTO>>;
}
