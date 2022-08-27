using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Database;
using SampleProject.Payment.Database.Entities;

namespace SampleProject.Payment.Database.Migrations
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