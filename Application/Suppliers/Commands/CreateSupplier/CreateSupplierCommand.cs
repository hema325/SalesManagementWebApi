namespace Application.Suppliers.Commands.CreateSupplier
{
    public record CreateSupplierCommand(string Name, string Address, string? Notes, List<string> PhoneNumbers) : IRequest<int>;
}
