using SampleProject.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Movie.Database.Entities
{
    [Table("RatingEntity")]
    public class RatingEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public double AverageRating { get; set; }
        public int NumVotes { get; set; }
        public string MovieId { get; set; }
        public virtual ICollection<MovieEntity> Movies { get; set; }
    }
}