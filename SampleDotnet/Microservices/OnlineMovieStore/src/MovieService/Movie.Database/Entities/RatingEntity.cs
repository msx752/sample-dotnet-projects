using SampleDotnet.RepositoryFactory.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Database.Entities
{
    [Table("RatingEntity")]
    public class RatingEntity : IHasDateTimeOffset
    {
        public Guid Id { get; set; }
        public double AverageRating { get; set; }
        public long NumVotes { get; set; }
        public string MovieId { get; set; }
        public virtual ICollection<MovieEntity> Movies { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}