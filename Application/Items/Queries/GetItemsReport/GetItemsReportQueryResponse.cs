using Application.Categories.Common;
using Application.Companies.Common;
using Application.Stocks.Common;
using Application.Suppliers.Common;
using Application.Units.Common;

namespace Application.Items.Queries.GetItemsReport
{
    public class GetItemsReportQueryResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int BuyingQuantity { get; init; }
        public int SellingQuantity { get; init; }
        public int AvailableQuantity => BuyingQuantity - SellingQuantity;
        public List<decimal> BuyingPrices { get; init; }
        public List<decimal> SellingPrices { get; init; }
        public decimal Profit { get; init; }
        public string Barcode { get; init; }
        public List<string> Images { get; init; }
        public DateTime CreatedOn { get; init; }

        public CompanyDTO Company { get; init; }
        public SupplierDTO Supplier { get; init; }
        public CategoryDTO Category { get; init; }
        public StockDTO Stock { get; init; }
        public UnitDTO Unit { get; init; }
    }
}
