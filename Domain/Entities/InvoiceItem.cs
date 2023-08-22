namespace Domain.Entities
{
    public class InvoiceItem
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
        public int ItemId { get; set; }
        public int InvoiceId { get; set; }

        public Item Item { get; set; }
        public Invoice Invoice { get; set; }
    }
}
