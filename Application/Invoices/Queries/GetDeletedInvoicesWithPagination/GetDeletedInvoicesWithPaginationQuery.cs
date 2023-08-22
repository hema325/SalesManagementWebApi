using Application.Invoices.Common;

namespace Application.Invoices.Queries.GetDeletedInvoicesWithPagination
{
    public record GetDeletedInvoicesWithPaginationQuery(int PageNumber, int PageSize): IRequest<PaginatedList<InvoiceDTO>>;
}
