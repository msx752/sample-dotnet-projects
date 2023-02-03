namespace SampleProject.Core.Interfaces.DbContexts
{
    public interface IBaseEntity
    {
        DateTimeOffset CreatedAt { get; set; }

        DateTimeOffset? UpdatedAt { get; set; }
    }
}