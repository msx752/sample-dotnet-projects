using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Database;
using SampleProject.Identity.Database.Entities;

namespace SampleProject.Identity.Core.Migrations
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