namespace SampleProject.Contract
{
    public interface IRequestMessage
    {
        public string ActivityId { get; set; }
        public Guid ActivityUserId { get; set; }
    }
}