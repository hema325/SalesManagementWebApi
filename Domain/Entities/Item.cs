namespace Domain.Entities
{
    public class Item: AuditableEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public string Barcode { get; set; }
        public List<Image> Images { get; set; }

        public int CompanyId { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public int StockId { get; set; }
        public int UnitId { get; set; }

        public Company Company { get; set; }
        public Supplier Supplier { get; set; }
        public Category Category { get; set; }
        public Stock Stock { get; set; }
        public ItemUnit Unit { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
