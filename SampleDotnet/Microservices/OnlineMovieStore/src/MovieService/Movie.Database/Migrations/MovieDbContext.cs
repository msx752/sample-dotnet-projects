using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Database;
using SampleProject.Movie.Database.Entities;

namespace SampleProject.Movie.Database.Migrations
{
    public class MovieDbContext : SampBaseContext
    {
        public DbSet<RatingEntity> Ratings { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<WriterEntity> Writers { get; set; }
        public DbSet<DirectorEntity> Directors { get; set; }
        public DbSet<MovieWriterEntity> MovieWriters { get; set; } //for many to many relations
        public DbSet<MovieDirectorEntity> MovieDirectors { get; set; } //for many to many relations
        public DbSet<MovieCategoryEntity> MovieCategories { get; set; } //for many to many relations

        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieCategoryEntity>().HasKey(f => new { f.MovieId, f.CategoryId });

            modelBuilder.Entity<MovieDirectorEntity>().HasKey(f => new { f.MovieId, f.DirectorId });

            modelBuilder.Entity<MovieWriterEntity>().HasKey(f => new { f.MovieId, f.WriterId });

            base.OnModelCreating(modelBuilder);
        }
    }
}