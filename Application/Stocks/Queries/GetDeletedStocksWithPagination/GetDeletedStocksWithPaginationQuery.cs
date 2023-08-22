using Application.Stocks.Common;

namespace Application.Stocks.Queries.GetDeletedStocksWithPagination
{
    public record GetDeletedStocksWithPaginationQuery(int PageNumber, int PageSize): IRequest<PaginatedList<StockDTO>>;
}
