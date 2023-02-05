using SampleProject.Database.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Database.Entities
{
    [Table("RefreshTokenEntity")]
    public class RefreshTokenEntity : IHasTimestamps
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string RefreshToken { get; set; }
        public virtual UserEntity User { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}