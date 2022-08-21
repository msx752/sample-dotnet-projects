using Newtonsoft.Json;

namespace Samp.Basket.API.Models.Dtos
{
    public class CartItemDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("productid")]
        public string ProductId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("salesprice")]
        public double SalesPrice { get; set; }
    }
}