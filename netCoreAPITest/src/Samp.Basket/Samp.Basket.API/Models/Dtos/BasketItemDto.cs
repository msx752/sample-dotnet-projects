namespace Samp.Basket.API.Models.Dtos
{
    public class BasketItemDto
    {
        public Guid Id { get; set; }

        public string ItemId { get; set; }
        public string Title { get; set; }
        public double SalesPrice { get; set; }
    }
}