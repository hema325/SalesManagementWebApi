using Application.Invoices.Common;

namespace Application.Invoices.Queries.GetInvoiceById
{
    public record GetInvoiceByIdQuery(int Id): IRequest<InvoiceDTO>;
}
