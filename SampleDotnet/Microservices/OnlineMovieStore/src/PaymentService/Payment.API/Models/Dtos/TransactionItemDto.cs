using Newtonsoft.Json;

namespace SampleProject.Payment.API.Models.Dtos
{
    public class TransactionItemDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("productid")]
        public string ProductId { get; set; }

        [JsonProperty("producttitle")]
        public string ProductTitle { get; set; }

        [JsonProperty("productprice")]
        public double ProductPrice { get; set; }

        [JsonProperty("productpricecurrency")]
        public string ProductPriceCurrency { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("calculatedprice")]
        public string CalculatedPrice { get; set; }

        [JsonProperty("transactionid")]
        public Guid TransactionId { get; set; }

        [JsonProperty("createdat")]
        public DateTimeOffset CreatedAt { get; set; }
    }
}