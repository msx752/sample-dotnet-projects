using Samp.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Movie.Database.Entities
{
    [Table("DirectorEntity")]
    public class DirectorEntity : BaseEntity
    {
        public DirectorEntity()
        {
            MovieDirectors = new HashSet<MovieDirectorEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string FullName { get; set; }
        public virtual ICollection<MovieDirectorEntity> MovieDirectors { get; set; }
    }
}