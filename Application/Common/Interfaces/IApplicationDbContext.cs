using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Client> Clients { get; }
        DbSet<Company> Companies { get; }
        DbSet<Supplier> Suppliers { get; }
        DbSet<ItemUnit> Units { get; }
        DbSet<Stock> Stocks { get; }
        DbSet<Category> Categories { get; }
        DbSet<Item> Items { get; }
        DbSet<Invoice> Invoices { get; }
        DbSet<InvoiceItem> InvoiceItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
