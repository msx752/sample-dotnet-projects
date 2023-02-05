using Identity.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Database
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        //roles, permissions etc..
    }
}