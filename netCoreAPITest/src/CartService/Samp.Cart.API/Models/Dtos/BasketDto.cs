using Samp.Basket.API.Models.Dtos;
using Samp.Cart.Database.Enums;

namespace Samp.Cart.API.Models.Dtos
{
    public class BasketDto
    {
        public BasketDto()
        {
            Items = new List<BasketItemDto>();
        }

        public Guid Id { get; set; }
        private CartStatus Satus { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}