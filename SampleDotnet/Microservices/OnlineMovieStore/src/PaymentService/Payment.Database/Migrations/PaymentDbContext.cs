using Microsoft.EntityFrameworkCore;
using Samp.Core.Database;
using Samp.Payment.Database.Entities;

namespace Samp.Payment.Database.Migrations
{
    public class PaymentDbContext : SampBaseContext
    {
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<TransactionItemEntity> TransactionItems { get; set; }

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }
    }
}