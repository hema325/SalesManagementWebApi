

namespace Domain.Entities
{
    public class Stock: AuditableEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Notes { get; set; }
    }
}
