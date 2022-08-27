namespace SampleProject.Contract.Payment.Cart
{
    public class CartItemEntityResponseMessage
    {
        public string ProductId { get; set; }
        public string Title { get; set; }
        public double SalesPrice { get; set; }
        public string SalesPriceCurrency { get; set; }
    }
}