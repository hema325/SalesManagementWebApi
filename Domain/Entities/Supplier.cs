namespace Domain.Entities
{
    public class Supplier: AuditableEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Notes { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
    }
}
