using Application.Stocks.Common;

namespace Application.Stocks.Queries.GetStockById
{
    public record GetStockByIdQuery(int Id):IRequest<StockDTO>;
}
