using Samp.Basket.Database.Enums;
using Samp.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Basket.Database.Entities
{
    [Table("BasketEntity")]
    public class BasketEntity : BaseEntity
    {
        public BasketEntity()
        {
            Items = new HashSet<BasketItemEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public BasketStatus Satus { get; set; }
        public Guid UserId { get; set; }
        public virtual ICollection<BasketItemEntity> Items { get; set; }
    }
}