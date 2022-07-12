using Microsoft.EntityFrameworkCore;
using Samp.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Samp.Core.Database
{
    public class SampBaseContext : DbContext
    {
        public DbSet<AuditEntity> Audits { get; set; }

        public SampBaseContext()
            : base()
        {
            UpdateSettings();
        }

        public bool IsUseAudit { get; set; }

        public SampBaseContext(DbContextOptions options)
            : base(options)
        {
            UpdateSettings();
        }

        public virtual void UpdateSettings()
        {
            IsUseAudit = true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public int SaveChanges(string userId)
        {
            OnBeforeSaveChanges(userId);
            return base.SaveChanges();
        }

        public new int SaveChanges()
        {
            throw new NotSupportedException();
        }

        public int SaveChanges(string userId, bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaveChanges(userId);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public new int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new NotSupportedException();
        }

        public Task<int> SaveChangesAsync(string userId, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges(userId);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public new Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }

        public Task<int> SaveChangesAsync(string userId, CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges(userId);
            return base.SaveChangesAsync(cancellationToken);
        }

        public new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }

        private void OnBeforeSaveChanges(string userId)
        {
            if (!IsUseAudit)
                return;

            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditEntity || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name.Replace("Entity", "s");
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = Enums.AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = Enums.AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = Enums.AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries)
            {
                Audits.Add(auditEntry.ToAudit());
            }
        }
    }
}