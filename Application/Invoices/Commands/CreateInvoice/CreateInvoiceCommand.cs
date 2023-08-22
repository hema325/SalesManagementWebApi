
namespace Application.Invoices.Commands.CreateInvoice
{
    public record CreateInvoiceCommand(decimal PaidUp,
                                       decimal Discount,
                                       Domain.Enums.Invoices Type,
                                       int? BuyerId,
                                       int? SellerId,
                                       List<CreateInvoiceItemCommand> InvoiceItems) :IRequest<int>;

    public record CreateInvoiceItemCommand(decimal Price, int Quantity, int ItemId);
}
