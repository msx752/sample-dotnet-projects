using Newtonsoft.Json;

namespace Samp.Payment.API.Models.Dtos
{
    public class TransactionDto
    {
        public TransactionDto()
        {
            TransactionItems = new List<TransactionItemDto>();
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("userid")]
        public Guid UserId { get; set; }

        [JsonProperty("totalcalculatedprice")]
        public string TotalCalculatedPrice { get; set; }

        [JsonProperty("transactionitems")]
        public List<TransactionItemDto> TransactionItems { get; set; }
    }
}