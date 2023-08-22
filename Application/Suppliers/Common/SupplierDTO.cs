namespace Application.Suppliers.Common
{
    public class SupplierDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string? Notes { get; init; }
        public List<string> PhoneNumbers { get; init; }
        public DateTime CreatedOn { get; init; }
    }
}
