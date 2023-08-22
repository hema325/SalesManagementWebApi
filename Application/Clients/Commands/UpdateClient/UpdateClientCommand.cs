namespace Application.Clients.Commands.UpdateClient
{
    public record UpdateClientCommand(int Id,
                                      string Name,
                                      Gender Gender,
                                      DateTime DateOfBirth,
                                      IEnumerable<string> PhoneNumbers,
                                      string Address) : IRequest;
}
