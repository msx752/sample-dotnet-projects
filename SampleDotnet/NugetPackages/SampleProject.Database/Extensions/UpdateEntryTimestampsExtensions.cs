using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SampleProject.Database.Interfaces.Entities;

public static class UpdateEntryTimestampsExtensions
{
    public static void AutoUpdateEntryTimestamps(this DbContext context, bool disposing = false)
    {
        try
        {
            context.ChangeTracker.Tracked -= UpdateEntryTimestamps;
            if (!disposing)
                context.ChangeTracker.Tracked += UpdateEntryTimestamps;

            context.ChangeTracker.StateChanged -= UpdateEntryTimestamps;
            if (!disposing)
                context.ChangeTracker.StateChanged += UpdateEntryTimestamps;
        }
        catch (Exception e)
        {
        }
    }

    public static void UpdateEntryTimestamps(object sender, EntityEntryEventArgs e)
    {
        if (e.Entry.State == EntityState.Unchanged || e.Entry.State == EntityState.Detached)
            return;

        var requestId = System.Diagnostics.Activity.Current?.RootId ?? Guid.Empty.ToString();

        if (e.Entry.Entity is IHasTimestamps entityWithTimestamps)
        {
            switch (e.Entry.State)
            {
                case EntityState.Added:
                    entityWithTimestamps.CreatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entityWithTimestamps.UpdatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Deleted:
                    entityWithTimestamps.DeletedAt = DateTime.UtcNow;
                    break;
            }
        }
    }
}