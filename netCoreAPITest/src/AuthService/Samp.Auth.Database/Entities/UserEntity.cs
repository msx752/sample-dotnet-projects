using Samp.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Identity.Database.Entities
{
    [Table("Users")]
    public class UserEntity : BaseEntity
    {
        public UserEntity()
        {
            RefreshTokens = new HashSet<RefreshTokenEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; }
    }
}