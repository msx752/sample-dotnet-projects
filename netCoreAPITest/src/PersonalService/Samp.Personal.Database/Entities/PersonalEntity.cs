using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Database.Personal.Entities
{
    [Table("Personals")]
    public class PersonalEntity
    {
        [Required]
        public byte Age { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string NationalId { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
    }
}