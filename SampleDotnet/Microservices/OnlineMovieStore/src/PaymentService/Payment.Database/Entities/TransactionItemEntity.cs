using SampleProject.Database.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.Database.Entities
{
    [Table("TransactionItemEntity")]
    public class TransactionItemEntity : IHasTimestamps
    {
        public TransactionItemEntity()
        {
            Quantity = 1;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ProductId { get; set; }
        public string ProductTitle { get; set; }
        public double ProductPrice { get; set; }
        public string ProductPriceCurrency { get; set; }
        public int Quantity { get; set; }
        public string CalculatedPrice { get; set; }
        public Guid TransactionId { get; set; }
        public virtual TransactionEntity Transaction { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}