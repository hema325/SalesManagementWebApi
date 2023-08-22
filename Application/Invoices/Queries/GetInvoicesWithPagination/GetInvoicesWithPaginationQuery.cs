using Application.Invoices.Common;

namespace Application.Invoices.Queries.GetInvoicesWithPagination
{
    public record GetInvoicesWithPaginationQuery(int PageNumber,int PageSize): IRequest<PaginatedList<InvoiceDTO>>;
}
