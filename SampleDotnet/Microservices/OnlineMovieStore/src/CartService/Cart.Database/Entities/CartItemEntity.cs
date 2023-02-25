
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cart.Database.Entities
{
    [Table("CartItemEntity")]
    public class CartItemEntity 
    {
        public CartItemEntity()
        {
            SalesPriceCurrency = "usd";
            ProductDatabase = "movie";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ProductId { get; set; }
        public string Title { get; set; }
        public double SalesPrice { get; set; }
        public string SalesPriceCurrency { get; set; }
        public string ProductDatabase { get; set; }
        public Guid CartId { get; set; }
        public virtual CartEntity Cart { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}