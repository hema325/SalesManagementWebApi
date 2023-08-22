using Application.Items.Common;

namespace Application.Items.Queries.GetItemsWithPagination
{
    public record GetItemsWithPaginationQuery(int PageNumber, int PageSize):IRequest<PaginatedList<ItemDTO>>;
}
