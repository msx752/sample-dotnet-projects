
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Database.Entities
{
    [Table("MovieDirectorEntity")]
    public class MovieDirectorEntity 
    {
        public string MovieId { get; set; }
        public virtual MovieEntity Movie { get; set; }
        public string DirectorId { get; set; }
        public virtual DirectorEntity Director { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}