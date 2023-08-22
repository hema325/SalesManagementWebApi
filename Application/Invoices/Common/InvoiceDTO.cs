using Application.Clients.Common;
using Application.Suppliers.Common;

namespace Application.Invoices.Common
{
    public class InvoiceDTO
    {
        public int Id { get; init; }
        public decimal PaidUp { get; init; }
        public decimal Discount { get; init; }
        public string Type { get; init; }
        public DateTime CreatedOn { get; init; }

        public ClientDTO Buyer { get; init; }
        public SupplierDTO Seller { get; init; }
        public List<InvoiceItemDTO> InvoiceItems { get; init; }
    }
}
