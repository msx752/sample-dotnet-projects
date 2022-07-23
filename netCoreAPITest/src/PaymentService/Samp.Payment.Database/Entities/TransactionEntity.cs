using Samp.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp.Payment.Database.Entities
{
    [Table("TransactionItemEntity")]
    public class TransactionEntity : BaseEntity
    {
        public TransactionEntity()
        {
            TransactionItems = new HashSet<TransactionItemEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<TransactionItemEntity> TransactionItems { get; set; }
    }
}