using Samp.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Movie.Database.Entities
{
    [Table("MovieCategoryEntity")]
    public class MovieCategoryEntity : BaseEntity
    {
        public string MovieId { get; set; }
        public virtual MovieEntity Movie { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
    }
}