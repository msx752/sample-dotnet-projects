using SampleDotnet.RepositoryFactory.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Database.Entities
{
    [Table("WriterEntity")]
    public class WriterEntity : IHasDateTimeOffset
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
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}