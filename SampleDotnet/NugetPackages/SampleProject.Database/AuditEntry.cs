using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using SampleProject.Core.Entities;
using SampleProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Core.Database
{
    public class AuditEntry
    {
        public AuditEntry(string tableName, EntityState state)
        {
            Identifier = System.Diagnostics.Activity.Current?.RootId ?? Guid.Empty.ToString();
            PrimaryKeys = new Dictionary<string, object>();
            OldValues = new Dictionary<string, object>();
            NewValues = new Dictionary<string, object>();
            AffectedColumns = new List<string>();
            CreatedAt = DateTimeOffset.UtcNow;
            State = state;
            TableName = tableName;
        }

        public string TableName { get; }
        public Dictionary<string, object> PrimaryKeys { get; }
        public Dictionary<string, object> OldValues { get; }
        public Dictionary<string, object> NewValues { get; }
        public EntityState State { get; }
        public List<string> AffectedColumns { get; }
        public string Identifier { get; }
        public DateTimeOffset CreatedAt { get; }

        //public AuditEntity ToAudit()
        //{
        //    var dtnow = DateTimeOffset.UtcNow;
        //    var audit = new AuditEntity();
        //    audit.CreatedAt = dtnow;
        //    audit.Identifier = System.Diagnostics.Activity.Current?.RootId ?? Guid.Empty.ToString();
        //    audit.Type = AuditType;
        //    audit.TableName = TableName;
        //    audit.PrimaryKey = JsonConvert.SerializeObject(KeyValues);
        //    audit.OldValues = OldValues.Count == 0 ? "" : JsonConvert.SerializeObject(OldValues);
        //    audit.NewValues = NewValues.Count == 0 ? "" : JsonConvert.SerializeObject(NewValues);
        //    audit.AffectedColumns = ChangedColumns.Count == 0 ? "" : JsonConvert.SerializeObject(ChangedColumns);
        //    return audit;
        //}
    }
}