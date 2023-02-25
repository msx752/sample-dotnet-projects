namespace SampleDotnet.RepositoryFactory.Interfaces;

public interface IRepositoryEntryNotifier
{
    void RepositoryEntryEvent(object sender, EntityEntryEventArgs e, DbContext dbContext, IServiceProvider serviceProvider);
}