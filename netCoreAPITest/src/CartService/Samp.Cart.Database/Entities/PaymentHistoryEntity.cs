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
    [Table("PaymentHistoryEntity")]
    public class PaymentHistoryEntity : BaseEntity
    {
        public PaymentHistoryEntity()
        {
            WhenPaid = DateTimeOffset.UtcNow;
        }

        public Guid TransactionId { get; set; }
        public string MovieId { get; set; }
        public string UserId { get; set; }
        public decimal PaidUsdPrice { get; set; }
        public DateTimeOffset WhenPaid { get; set; }
    }
}