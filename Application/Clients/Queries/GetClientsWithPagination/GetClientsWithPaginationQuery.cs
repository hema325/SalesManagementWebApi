using Application.Clients.Common;

namespace Application.Clients.Queries.GetClientsWithPagination
{
    public record GetClientsWithPaginationQuery(int PageNumber, int PageSize): IRequest<PaginatedList<ClientDTO>>;
}
