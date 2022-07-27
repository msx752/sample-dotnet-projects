namespace Samp.Payment.API.Models.Dtos
{
    public class TransactionItemDto
    {
        public Guid Id { get; set; }

        public string ProductId { get; set; }
        public string ProductTitle { get; set; }
        public double ProductPrice { get; set; }
        public string ProductPriceCurrency { get; set; }
        public int Quantity { get; set; }
        public string CalculatedPrice { get; set; }

        public Guid TransactionId { get; set; }
    }
}