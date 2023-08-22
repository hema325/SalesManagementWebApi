using Application.Suppliers.Common;

namespace Application.Suppliers.Queries.GetDeletedSuppliersWithPagination
{
    public record GetDeletedSuppliersWithPaginationQuery(int PageNumber, int PageSize): IRequest<PaginatedList<SupplierDTO>>;
}
