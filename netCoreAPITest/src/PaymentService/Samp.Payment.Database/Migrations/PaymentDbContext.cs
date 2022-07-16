using Microsoft.EntityFrameworkCore;
using Samp.Core.Database;

namespace Samp.Payment.Database.Migrations
{
    public class PaymentDbContext : SampBaseContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }
    }
}