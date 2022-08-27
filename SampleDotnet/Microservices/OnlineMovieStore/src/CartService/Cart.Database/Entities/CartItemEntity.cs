using Cart.Database.Entities;
using SampleProject.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleProject.Cart.Database.Entities
{
    [Table("CartItemEntity")]
    public class CartItemEntity : BaseEntity
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
    }
}