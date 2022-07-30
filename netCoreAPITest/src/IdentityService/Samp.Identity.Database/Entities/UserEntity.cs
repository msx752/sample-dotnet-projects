using Samp.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Identity.Database.Entities
{
    [Table("UserEntity")]
    public class UserEntity : BaseEntity
    {
        public UserEntity()
        {
            RefreshTokens = new HashSet<RefreshTokenEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Username { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Surname { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; }
    }
}