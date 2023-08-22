using Domain.Common.Contracts;
using Infrastructure.Identity.Models;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    internal class ApplicationDbContext: IdentityDbContext<ApplicationUser,ApplicationRole,string>,IApplicationDbContext
    {
        private readonly IPublisher _publisher;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : base(options) 
        {
            _publisher = publisher;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=Default");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            AuditableEntityConfiguration.ConfigureAuditableEntity(modelBuilder);
            modelBuilder.AppendGlobalQueryFilter<AuditableEntity>(e => e.DeletedOn == null);
            base.OnModelCreating(modelBuilder);
        }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var affectedRows = await base.SaveChangesAsync(cancellationToken);
            await _publisher.DispatchDomainEventsAsync(this);

            return affectedRows;
        }

        public DbSet<Client> Clients { get; private set; }
        public DbSet<Company> Companies { get; private set; }
        public DbSet<Supplier> Suppliers { get; private set; }
        public DbSet<ItemUnit> Units { get; private set; }
        public DbSet<Stock> Stocks { get; private set; }
        public DbSet<Category> Categories { get; private set; }
        public DbSet<Item> Items { get; private set; }
        public DbSet<Invoice> Invoices { get; private set; }
        public DbSet<InvoiceItem> InvoiceItems { get; private set; }
    }
}
