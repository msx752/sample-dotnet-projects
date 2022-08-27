using Newtonsoft.Json;
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

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("status")]
        private CartStatus Satus { get; set; }

        [JsonProperty("items")]
        public List<CartItemDto> Items { get; set; }
    }
}