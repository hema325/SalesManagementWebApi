namespace Application.Invoices.Commands.UpdateInvoice
{
    public record UpdateInvoiceCommand(int Id,
                                       decimal PaidUp,
                                       decimal Discount,
                                       Domain.Enums.Invoices Type,
                                       int? BuyerId,
                                       int? SellerId,
                                       List<UpdateInvoiceItemCommand> InvoiceItems) : IRequest;

    public record UpdateInvoiceItemCommand(decimal Price, int Quantity, int ItemId);
}
