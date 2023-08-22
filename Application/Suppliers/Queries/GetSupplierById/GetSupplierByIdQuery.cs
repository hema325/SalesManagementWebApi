using Application.Suppliers.Common;

namespace Application.Suppliers.Queries.GetSupplierById
{
    public record GetSupplierByIdQuery(int Id): IRequest<SupplierDTO>;
}
