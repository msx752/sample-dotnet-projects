using Microsoft.EntityFrameworkCore;
using Samp.Core.Database;
using Samp.Identity.Database.Entities;

namespace Samp.Identity.Core.Migrations
{
    public class SampIdentityContext : SampBaseContext
    {
        public SampIdentityContext(DbContextOptions<SampIdentityContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        //roles, permissions etc..
    }
}