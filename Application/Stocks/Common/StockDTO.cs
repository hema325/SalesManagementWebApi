namespace Application.Stocks.Common
{
    public class StockDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string? Notes { get; init; }
        public DateTime CreatedOn { get; init; }
    }
}
