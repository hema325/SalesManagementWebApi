using Application.Clients.Common;

namespace Application.Clients.Queries.GetDeletedClientsWithPagination
{
    public record GetDeletedClientsWithPaginationQuery(int PageNumber, int PageSize): IRequest<PaginatedList<ClientDTO>>;
}
