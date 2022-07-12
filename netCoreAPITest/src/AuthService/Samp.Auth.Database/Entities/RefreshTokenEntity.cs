using Samp.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp.Identity.Database.Entities
{
    [Table("RefreshTokenEntity")]
    public class RefreshTokenEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public string RefreshToken { get; set; }
    }
}