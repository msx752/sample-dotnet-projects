using Microsoft.EntityFrameworkCore;
using Samp.Core.Database;
using Samp.Identity.Core.Entities;

namespace Samp.Identity.Core.Migrations
{
    public class SampIdentityContext : SampBaseContext
    {
        public SampIdentityContext(DbContextOptions<SampIdentityContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        //roles, permissions etc..
    }
}