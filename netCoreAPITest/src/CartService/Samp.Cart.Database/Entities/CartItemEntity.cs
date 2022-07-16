using Samp.Basket.Database.Entities;
using Samp.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Cart.Database.Entities
{
    [Table("CartItemEntity")]
    public class CartItemEntity : BaseEntity
    {
        public CartItemEntity()
        {
            SalesPriceCurrency = "usd";
            ItemDatabase = "Movies";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ItemId { get; set; }
        public string Title { get; set; }
        public double SalesPrice { get; set; }
        public string SalesPriceCurrency { get; set; }
        public string ItemDatabase { get; set; }
        public Guid BasketId { get; set; }
        public virtual CartEntity Basket { get; set; }
    }
}