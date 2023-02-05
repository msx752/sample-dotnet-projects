using SampleProject.Database.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Database.Entities
{
    [Table("DirectorEntity")]
    public class DirectorEntity : IHasTimestamps
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
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}