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
    [Table("PaymentHistoryEntity")]
    public class PaymentHistoryEntity : BaseEntity
    {
        public PaymentHistoryEntity()
        {
            WhenPaid = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; set; }
        public string ProductId { get; set; }
        public string ProductTitle { get; set; }
        public Guid UserId { get; set; }
        public decimal PaidUsdPrice { get; set; }
        public DateTimeOffset WhenPaid { get; set; }
    }
}