namespace Domain.Entities
{
    public class Invoice: AuditableEntity
    {
        public decimal PaidUp { get; set; }
        public decimal Discount { get; set; }
        public Invoices Type { get; set; }

        public int? BuyerId { get; set; }
        public int? SellerId { get; set; }

        public Client Buyer { get; set; }
        public Supplier Seller { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }

    }
}
