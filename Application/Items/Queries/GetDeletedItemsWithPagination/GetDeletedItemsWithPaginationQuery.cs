using Application.Items.Common;

namespace Application.Items.Queries.GetDeletedItemsWithPagination
{
    public record GetDeletedItemsWithPaginationQuery(int PageNumber,int PageSize): IRequest<PaginatedList<ItemDTO>>;
}
