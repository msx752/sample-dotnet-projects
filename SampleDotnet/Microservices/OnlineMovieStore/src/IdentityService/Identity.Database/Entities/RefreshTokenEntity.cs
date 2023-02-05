using SampleProject.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Database.Entities
{
    [Table("RefreshTokenEntity")]
    public class RefreshTokenEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string RefreshToken { get; set; }
        public virtual UserEntity User { get; set; }
        public Guid UserId { get; set; }
    }
}