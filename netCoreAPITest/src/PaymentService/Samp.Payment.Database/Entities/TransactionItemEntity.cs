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
    public class TransactionItemEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ProductId { get; set; }
        public string ProductTitle { get; set; }
        public double ProductPrice { get; set; }
        public string ProductPriceCurrency { get; set; }

        public Guid TransactionId { get; set; }
        public virtual TransactionEntity Transaction { get; set; }
    }
}