using Application.Items.Common;

namespace Application.Items.Queries.GetItemById
{
    public record GetItemByIdQuery(int Id): IRequest<ItemDTO>;
}
