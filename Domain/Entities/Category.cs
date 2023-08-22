namespace Domain.Entities
{
    public class Category: AuditableEntity
    {
        public string Name { get; set; }
        public string? Notes { get; set; }
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
    }
}
