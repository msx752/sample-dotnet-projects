using Samp.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Movie.Database.Entities
{
    [Table("CategoryEntity")]
    public class CategoryEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual ICollection<MovieCategoryEntity> Categories { get; set; }
    }
}