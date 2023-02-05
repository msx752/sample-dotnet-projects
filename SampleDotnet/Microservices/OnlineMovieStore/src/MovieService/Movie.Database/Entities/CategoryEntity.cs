using SampleProject.Database.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Database.Entities
{
    [Table("CategoryEntity")]
    public class CategoryEntity : IHasTimestamps
    {
        public CategoryEntity()
        {
            Categories = new HashSet<MovieCategoryEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }
        public virtual ICollection<MovieCategoryEntity> Categories { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}