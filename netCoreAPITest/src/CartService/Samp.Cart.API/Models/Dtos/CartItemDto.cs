namespace Samp.Basket.API.Models.Dtos
{
    public class CartItemDto
    {
        public Guid Id { get; set; }

        public string ProductId { get; set; }
        public string Title { get; set; }
        public double SalesPrice { get; set; }
    }
}