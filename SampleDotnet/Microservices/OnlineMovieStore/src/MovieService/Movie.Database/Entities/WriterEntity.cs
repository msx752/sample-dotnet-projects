using SampleProject.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Movie.Database.Entities
{
    [Table("WriterEntity")]
    public class WriterEntity : BaseEntity
    {
        public WriterEntity()
        {
            MovieWriters = new HashSet<MovieWriterEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string FullName { get; set; }
        public virtual ICollection<MovieWriterEntity> MovieWriters { get; set; }
    }
}