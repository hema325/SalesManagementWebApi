using Application.Clients.Common;

namespace Application.Clients.Queries.GetClientById
{
    public record GetClientByIdQuery(int Id): IRequest<ClientDTO>;
}
