namespace Domain.Entities
{
    public class ItemUnit : AuditableEntity
    {
        public string Name { get; set; }
        public string? Notes { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
