using Application.Categories.Common;

namespace Application.Categories.Queries.GetDeletedCategoriesWithPagination
{
    public record GetDeletedCategoriesWithPaginationQuery(int PageNumber,int PageSize): IRequest<PaginatedList<CategoryDTO>>;
}
