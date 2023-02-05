namespace SampleProject.Contract.Payment.Cart
{
    public class CartEntityResponseMessage : IResponseMessage
    {
        public CartEntityResponseMessage()
        {
            Items = new();
        }

        public Guid Id { get; set; }
        public string BusErrorMessage { get; set; }
        public List<CartItemEntityResponseMessage> Items { get; set; }
    }
}