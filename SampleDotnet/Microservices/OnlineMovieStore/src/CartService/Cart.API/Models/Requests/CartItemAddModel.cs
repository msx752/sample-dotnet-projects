using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SampleProject.Cart.API.Models.Requests
{
    public class CartItemAddModel
    {
        [Required]
        [JsonProperty("productid")]
        public string ProductId { get; set; }

        [Required]
        [JsonProperty("productdatabase")]
        public string ProductDatabase { get; set; }
    }
}