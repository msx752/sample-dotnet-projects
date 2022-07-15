using Microsoft.EntityFrameworkCore;
using Samp.Basket.Database.Entities;
using Samp.Core.Database;

namespace Samp.Basket.Database.Migrations
{
    public class BasketDbContext : SampBaseContext
    {
        public DbSet<BasketEntity> Baskets { get; set; }
        public DbSet<BasketItemEntity> BasketItems { get; set; }

        public BasketDbContext(DbContextOptions<BasketDbContext> options)
            : base(options)
        {
        }
    }
}