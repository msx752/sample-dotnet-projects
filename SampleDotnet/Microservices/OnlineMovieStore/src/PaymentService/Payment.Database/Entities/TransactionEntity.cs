
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.Database.Entities
{
    [Table("TransactionEntity")]
    public class TransactionEntity 
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
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}