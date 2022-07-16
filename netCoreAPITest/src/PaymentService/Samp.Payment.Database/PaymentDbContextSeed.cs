using Samp.Core.Database;
using Samp.Core.Interfaces.Repositories;
using Samp.Payment.Database.Migrations;

namespace Samp.Payment.Database
{
    public class PaymentDbContextSeed : ContextSeed<PaymentDbContext>
    {
        public PaymentDbContextSeed(ISharedRepository<PaymentDbContext> connection)
            : base(connection)
        {
        }

        public override void CommitSeed()
        {
        }
    }
}