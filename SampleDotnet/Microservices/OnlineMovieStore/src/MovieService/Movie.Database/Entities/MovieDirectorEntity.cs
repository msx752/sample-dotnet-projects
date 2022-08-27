using SampleProject.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Movie.Database.Entities
{
    [Table("MovieDirectorEntity")]
    public class MovieDirectorEntity : BaseEntity
    {
        public string MovieId { get; set; }
        public virtual MovieEntity Movie { get; set; }
        public string DirectorId { get; set; }
        public virtual DirectorEntity Director { get; set; }
    }
}