using SampleProject.Basket.Database.Migrations;
using SampleProject.Core.Database;
using SampleProject.Core.Interfaces.Repositories;

namespace Cart.Database
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