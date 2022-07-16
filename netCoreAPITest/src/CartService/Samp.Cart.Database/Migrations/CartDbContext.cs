using Microsoft.EntityFrameworkCore;
using Samp.Basket.Database.Entities;
using Samp.Cart.Database.Entities;
using Samp.Core.Database;

namespace Samp.Basket.Database.Migrations
{
    public class CartDbContext : SampBaseContext
    {
        public DbSet<CartEntity> Baskets { get; set; }
        public DbSet<CartItemEntity> BasketItems { get; set; }

        public CartDbContext(DbContextOptions<CartDbContext> options)
            : base(options)
        {
        }
    }
}