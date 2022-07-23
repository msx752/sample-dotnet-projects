using Samp.Cart.Database.Entities;
using Samp.Cart.Database.Enums;
using Samp.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Basket.Database.Entities
{
    [Table("CartEntity")]
    public class CartEntity : BaseEntity
    {
        public CartEntity()
        {
            Items = new HashSet<CartItemEntity>();
            Satus = CartStatus.Open;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public CartStatus Satus { get; set; }
        public Guid UserId { get; set; }
        public virtual ICollection<CartItemEntity> Items { get; set; }
    }
}