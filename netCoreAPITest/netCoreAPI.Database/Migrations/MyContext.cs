using Microsoft.EntityFrameworkCore;
using netCoreAPI.Database.Entities;

namespace netCoreAPI.Database.Migrations
{
    public class MyContext : DbContext
    {
        public MyContext() : base()
        {
            SetChangesTrackerMode();
        }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            SetChangesTrackerMode();
        }

        public DbSet<PersonalEntity> Personals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             *
             * these are already defined in personal.cs file so not required
             *
             * but i added for the demostration
             *
             * (ENTITY FRAMEWORK FLUENT API)
             *
             * */
            modelBuilder.Entity<PersonalEntity>().ToTable("Personals");
            modelBuilder.Entity<PersonalEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<PersonalEntity>().Property(x => x.Age).IsRequired();
            modelBuilder.Entity<PersonalEntity>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<PersonalEntity>().Property(x => x.Surname).IsRequired().HasMaxLength(50);
            /************************************************************************************/

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// this behave must be observe before usage of the relational entity updates
        /// </summary>
        private void SetChangesTrackerMode()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        }
    }
}