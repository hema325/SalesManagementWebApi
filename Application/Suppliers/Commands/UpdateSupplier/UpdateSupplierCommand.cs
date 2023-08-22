namespace Application.Suppliers.Commands.UpdateSupplier
{
    public record UpdateSupplierCommand(int Id, string Name, string Address, string? Notes, List<string> PhoneNumbers) : IRequest;
}
