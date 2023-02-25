
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Database.Entities
{
    [Table("MovieWriterEntity")]
    public class MovieWriterEntity 
    {
        public string MovieId { get; set; }
        public virtual MovieEntity Movie { get; set; }
        public string WriterId { get; set; }
        public virtual WriterEntity Writer { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}