using Microsoft.EntityFrameworkCore;
using Samp.Core.Database;

namespace Samp.Cart.Database.Migrations
{
    public class CartDbContext : SampBaseContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options)
            : base(options)
        {
        }
    }
}