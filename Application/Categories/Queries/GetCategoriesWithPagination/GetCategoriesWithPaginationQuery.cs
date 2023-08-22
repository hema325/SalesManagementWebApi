using Application.Categories.Common;

namespace Application.Categories.Queries.GetCategoriesWithPagination
{
    public record GetCategoriesWithPaginationQuery(int PageNumber,int PageSize): IRequest<PaginatedList<CategoryDTO>>;
}
