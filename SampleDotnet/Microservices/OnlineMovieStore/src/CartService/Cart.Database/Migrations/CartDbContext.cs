using Cart.Database.Entities;
using Microsoft.EntityFrameworkCore;
using SampleProject.Cart.Database.Entities;
using SampleProject.Core.Database;

namespace SampleProject.Basket.Database.Migrations
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