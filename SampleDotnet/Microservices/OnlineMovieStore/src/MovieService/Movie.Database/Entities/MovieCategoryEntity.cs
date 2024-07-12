
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Database.Entities
{
    [Table("MovieCategoryEntity")]
    public class MovieCategoryEntity 
    {
        public string MovieId { get; set; }
        public virtual MovieEntity Movie { get; set; }
        public long CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}