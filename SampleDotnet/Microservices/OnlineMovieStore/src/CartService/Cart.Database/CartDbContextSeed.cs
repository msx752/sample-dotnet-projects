using Samp.Basket.Database.Migrations;
using Samp.Core.Database;
using Samp.Core.Interfaces.Repositories;

namespace Samp.Cart.Database
{
    public class CartDbContextSeed : ContextSeed<CartDbContext>
    {
        public CartDbContextSeed(IUnitOfWork<CartDbContext> connection)
            : base(connection)
        {
        }

        public override void CommitSeed()
        {
        }
    }
}