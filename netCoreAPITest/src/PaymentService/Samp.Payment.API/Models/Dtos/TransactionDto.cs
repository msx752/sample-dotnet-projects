namespace Samp.Payment.API.Models.Dtos
{
    public class TransactionDto
    {
        public TransactionDto()
        {
            TransactionItems = new List<TransactionItemDto>();
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string TotalCalculatedPrice { get; set; }

        public List<TransactionItemDto> TransactionItems { get; set; }
    }
}