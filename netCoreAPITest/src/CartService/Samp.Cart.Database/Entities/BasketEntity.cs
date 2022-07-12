using Samp.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp.Cart.Database.Entities
{
    [Table("BasketEntity")]
    public class BasketEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } //TransactionId

        public string UserId { get; set; }
        public string MovieIds { get; set; } //bind to PaymentHistoryEntity
    }
}