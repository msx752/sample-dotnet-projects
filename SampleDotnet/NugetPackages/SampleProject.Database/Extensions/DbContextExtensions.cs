public static class DbContextExtensions
{
    public static IEnumerable<AuditEntry> DetectEntryChanges(this DbContext dbContext)
    {
        if (!dbContext.ChangeTracker.AutoDetectChangesEnabled)
            dbContext.ChangeTracker.DetectChanges();

        foreach (var entry in dbContext.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                continue;

            var auditEntry = new AuditEntry(entry.Entity.GetType().Name, entry.State);

            foreach (var property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.PrimaryKeys[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (auditEntry.State)
                {
                    case EntityState.Added:
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;

                    case EntityState.Deleted:
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;

                    case EntityState.Modified:
                        if (property.IsModified && property.CurrentValue?.ToString() != property.OriginalValue?.ToString())
                        {
                            auditEntry.AffectedColumns.Add(propertyName);
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        else
                        {
                            continue;
                        }
                        break;
                }
            }

            yield return auditEntry;
        }
    }
}