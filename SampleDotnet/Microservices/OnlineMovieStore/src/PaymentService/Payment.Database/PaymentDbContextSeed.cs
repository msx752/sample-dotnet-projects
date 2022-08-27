using SampleProject.Core.Database;
using SampleProject.Core.Interfaces.Repositories;
using SampleProject.Payment.Database.Migrations;

namespace SampleProject.Payment.Database
{
    public class PaymentDbContextSeed : ContextSeed<PaymentDbContext>
    {
        public PaymentDbContextSeed(IUnitOfWork<PaymentDbContext> connection)
            : base(connection)
        {
        }

        public override void CommitSeed()
        {
        }
    }
}