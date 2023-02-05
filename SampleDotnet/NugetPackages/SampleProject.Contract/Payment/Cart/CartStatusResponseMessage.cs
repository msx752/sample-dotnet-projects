namespace SampleProject.Contract.Payment.Cart
{
    public class CartStatusResponseMessage : IResponseMessage
    {
        public string BusErrorMessage { get; set; }
        public string ActivityId { get; set; }
        public Guid ActivityUserId { get; set; }
    }
}