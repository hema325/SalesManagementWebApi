namespace Application.Stocks.Commands.UpdateStock
{
    public record UpdateStockCommand(int Id, string Name,string Address, string Notes):IRequest;
}
