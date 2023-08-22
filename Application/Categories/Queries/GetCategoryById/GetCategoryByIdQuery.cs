using Application.Categories.Common;

namespace Application.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(int Id): IRequest<CategoryDTO>;
}
