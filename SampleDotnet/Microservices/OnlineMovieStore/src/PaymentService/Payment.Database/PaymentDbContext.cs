using Microsoft.EntityFrameworkCore;
using Payment.Database.Entities;

namespace Payment.Database
{
    public class PaymentDbContext : DbContext
    {
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<TransactionItemEntity> TransactionItems { get; set; }

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }
    }
}