using Microsoft.EntityFrameworkCore;

namespace Demo.Models;

public class DemoDbContext : DbContext
{
    public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
    {
    }
    
    public DbSet<Invoice>? Invoices { get; set; }
    
    public DbSet<InvoiceItem>? InvoiceItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasMany(x => x.InvoiceItems)
                  .WithOne(x => x.Invoice)
                  .HasForeignKey(x => x.InvoiceId);
        });
        
        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(x => x.Id);
        });
    }
}