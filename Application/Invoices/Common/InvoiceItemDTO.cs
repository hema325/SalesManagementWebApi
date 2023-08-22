using Application.Items.Common;
using Application.Units.Common;

namespace Application.Invoices.Common
{
    public class InvoiceItemDTO
    {
        public decimal Price { get; init; }
        public int Quantity { get; init; }

        public UnitDTO Unit { get; set; }
        public ItemDTO Item { get; set; }
    }
}
