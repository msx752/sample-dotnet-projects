using Samp.Basket.API.Models.Dtos;
using Samp.Cart.Database.Enums;

namespace Samp.Cart.API.Models.Dtos
{
    public class CartDto
    {
        public CartDto()
        {
            Items = new List<CartItemDto>();
        }

        public Guid Id { get; set; }
        private CartStatus Satus { get; set; }
        public List<CartItemDto> Items { get; set; }
    }
}