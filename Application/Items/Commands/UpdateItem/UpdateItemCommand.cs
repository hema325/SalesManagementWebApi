namespace Application.Items.Commands.UpdateItem
{
    public record UpdateItemCommand(int Id,
                                    string Name,
                                    decimal Price,
                                    string? Notes,
                                    bool IsActive,
                                    string Barcode,
                                    int CompanyId,
                                    int SupplierId,
                                    int CategoryId,
                                    int StockId,
                                    int UnitId) : IRequest;
}
