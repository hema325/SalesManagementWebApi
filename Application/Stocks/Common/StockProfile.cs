namespace Application.Stocks.Common
{
    internal class StockProfile: Profile
    {
        public StockProfile()
        {
            CreateMap<Stock, StockDTO>();
        }
    }
}
