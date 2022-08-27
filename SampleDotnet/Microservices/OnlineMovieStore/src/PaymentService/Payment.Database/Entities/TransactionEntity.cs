﻿using Samp.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        /// <summary>
        /// payer
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// paid
        /// </summary>
        public Guid CartId { get; set; }

        public string TotalCalculatedPrice { get; set; }
        public virtual ICollection<TransactionItemEntity> TransactionItems { get; set; }
    }
}