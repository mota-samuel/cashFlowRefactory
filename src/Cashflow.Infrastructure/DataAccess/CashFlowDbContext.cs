using Cashflow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cashflow.Infrastructure.DataAccess;
public class CashFlowDbContext : DbContext
{
    public CashFlowDbContext(DbContextOptions options) : base(options){}

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);
            entity.Property(u => u.Id).ValueGeneratedOnAdd(); // Id continua identity
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasOne(e => e.User)
              .WithMany()
              .HasForeignKey(e => e.UserId)     // Expense.UserId (Guid)
              .HasPrincipalKey(u => u.UserId)   // aponta para User.UserId (Guid)
              .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
