using Microsoft.AspNetCore.Http;

namespace Application.Items.Commands.CreateItem
{
    public record CreateItemCommand(string Name,
                                    decimal Price,
                                    string? Notes,
                                    bool IsActive,
                                    string Barcode,
                                    List<IFormFile> Images,
                                    int CompanyId,
                                    int SupplierId,
                                    int CategoryId,
                                    int StockId,
                                    int UnitId) : IRequest<int>;
}
