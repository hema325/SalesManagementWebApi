namespace Domain.Entities
{
    public class Company:AuditableEntity
    {
        public string Name { get; set; }
        public string? Notes { get; set; }
    }
}
