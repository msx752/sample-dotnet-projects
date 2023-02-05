namespace SampleProject.Database.Interfaces.Entities
{
    public interface IHasTimestamps
    {
        DateTimeOffset? CreatedAt { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
    }
}