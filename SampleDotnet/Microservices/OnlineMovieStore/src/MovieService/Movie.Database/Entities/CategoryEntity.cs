using SampleProject.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Movie.Database.Entities
{
    [Table("CategoryEntity")]
    public class CategoryEntity : BaseEntity
    {
        public CategoryEntity()
        {
            Categories = new HashSet<MovieCategoryEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual ICollection<MovieCategoryEntity> Categories { get; set; }
    }
}