
using Microsoft.AspNetCore.Http;

namespace Application.Clients.Commands.CreateClient
{
    public record CreateClientCommand(string Name,
                                      string Gender,
                                      DateTime DateOfBirth,
                                      IEnumerable<string> PhoneNumbers,
                                      string Address,
                                      IEnumerable<IFormFile> Images) : IRequest<int>;
}
