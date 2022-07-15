using Samp.Basket.Database.Migrations;
using Samp.Core.Database;
using Samp.Core.Interfaces.Repositories;

namespace Samp.Basket.Database
{
    public class BasketDbContextSeed : ContextSeed<BasketDbContext>
    {
        public BasketDbContextSeed(ISharedRepository<BasketDbContext> connection)
            : base(connection)
        {
        }

        public override void CommitSeed()
        {
        }
    }
}