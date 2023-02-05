using Microsoft.EntityFrameworkCore;

namespace SampleProject.Database.Audits
{
    public class AuditEntry : IDisposable
    {
        private bool disposedValue;

        public AuditEntry(string tableName, EntityState state)
        {
            Identifier = System.Diagnostics.Activity.Current?.RootId ?? Guid.Empty.ToString();
            PrimaryKeys = new Dictionary<string, object>();
            OldValues = new Dictionary<string, object>();
            NewValues = new Dictionary<string, object>();
            AffectedColumns = new List<string>();
            CreatedAt = DateTimeOffset.Now;
            State = state;
            TableName = tableName;
        }

        public List<string>? AffectedColumns { get; private set; }
        public DateTimeOffset? CreatedAt { get; private set; }
        public string? Identifier { get; private set; }
        public Dictionary<string, object>? NewValues { get; private set; }
        public Dictionary<string, object>? OldValues { get; private set; }
        public Dictionary<string, object>? PrimaryKeys { get; private set; }
        public EntityState? State { get; private set; }
        public string? TableName { get; private set; }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    PrimaryKeys?.Clear();
                    OldValues?.Clear();
                    NewValues?.Clear();
                    AffectedColumns?.Clear();
                }

                TableName = null;
                PrimaryKeys = null;
                OldValues = null;
                NewValues = null;
                State = null;
                AffectedColumns = null;
                Identifier = null;
                CreatedAt = null;

                disposedValue = true;
            }
        }
    }
}