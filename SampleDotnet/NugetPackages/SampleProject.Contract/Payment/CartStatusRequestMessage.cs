namespace SampleProject.Contract.Payment
{
    public class CartStatusRequestMessage : IRequestMessage
    {
        public Guid CartId { get; set; }

        /// <summary>
        /// CartStatus enum types; Open, LockedOnPayment
        /// </summary>
        public string CartStatus { get; set; }

        public string ActivityId { get; set; }
        public Guid ActivityUserId { get; set; }
    }
}