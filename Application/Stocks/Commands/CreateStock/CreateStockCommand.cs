namespace Application.Stocks.Commands.CreateStock
{
    public record CreateStockCommand(string Name, string Address, string Notes): IRequest<int>;
}
