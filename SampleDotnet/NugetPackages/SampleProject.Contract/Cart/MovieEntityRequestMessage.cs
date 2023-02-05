namespace SampleProject.Contract.Cart.Requests
{
    public class MovieEntityRequestMessage : IRequestMessage
    {
        public string ProductDatabase { get; set; }
        public string ProductId { get; set; }
        public string ActivityId { get; set; }
        public Guid ActivityUserId { get; set; }
    }
}