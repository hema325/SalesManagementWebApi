using Application.Categories.Common;
using Application.Companies.Common;
using Application.Invoices.Common;
using Application.Stocks.Common;
using Application.Suppliers.Common;

namespace Application.Items.Common
{
    public class ItemDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Quantity { get; init; }
        public decimal Price { get; init; }
        public string? Notes { get; init; }
        public bool IsActive { get; init; }
        public string Barcode { get; init; }
        public List<string> Images { get; init; }
        public DateTime CreatedOn { get; init; }

        public CompanyDTO Company { get; init; }
        public SupplierDTO Supplier { get; init; }
        public CategoryDTO Category { get; init; }
        public StockDTO Stock { get; init; }

        public List<InvoiceItemDTO> InvoiceItems { get; init; }
    }
}
