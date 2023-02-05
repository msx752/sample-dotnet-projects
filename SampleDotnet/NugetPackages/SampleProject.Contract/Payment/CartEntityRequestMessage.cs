namespace SampleProject.Contract.Payment
{
    public class CartEntityRequestMessage : IRequestMessage
    {
        public string ActivityId { get; set; }
        public Guid ActivityUserId { get; set; }
        public Guid CartId { get; set; }
    }
}