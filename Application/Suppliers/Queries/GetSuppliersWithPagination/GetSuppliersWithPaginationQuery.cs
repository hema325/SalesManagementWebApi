using Application.Suppliers.Common;

namespace Application.Suppliers.Queries.GetSuppliersWithPagination
{
    public record GetSuppliersWithPaginationQuery(int PageNumber, int PageSize): IRequest<PaginatedList<SupplierDTO>>;
}
