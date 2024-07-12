
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Database.Entities
{
    [Table("UserEntity")]
    public class UserEntity 
    {
        public UserEntity()
        {
            RefreshTokens = new HashSet<RefreshTokenEntity>();
        }

        [StringLength(250)]
        public string Email { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; }

        [StringLength(250)]
        public string Surname { get; set; }

        [Required]
        [StringLength(250)]
        public string Username { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}