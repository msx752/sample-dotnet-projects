using Samp.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Identity.Core.Entities
{
    [Table("Users")]
    public class UserEntity : IUserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}