using Microsoft.EntityFrameworkCore;
using Samp.Core.Database;
using Samp.Identity.Database.Entities;

namespace Samp.Identity.Core.Migrations
{
    public class IdentityDbContext : SampBaseContext
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