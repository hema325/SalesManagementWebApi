using Application.Stocks.Common;

namespace Application.Stocks.Queries.GetStocksWithPagination
{
    public record GetStocksWithPaginationQuery(int PageNumber,int PageSize): IRequest<PaginatedList<StockDTO>>;
}
