using Microsoft.EntityFrameworkCore;

namespace Samp.Core.Database
{
    public class SampBaseContext : DbContext
    {
        public SampBaseContext()
            : base()
        {
            SetChangesTrackerMode();
        }

        public SampBaseContext(DbContextOptions options) : base(options)
        {
            SetChangesTrackerMode();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// this behave must be observe before usage of the relational entity updates
        /// </summary>
        protected virtual void SetChangesTrackerMode()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        }
    }
}