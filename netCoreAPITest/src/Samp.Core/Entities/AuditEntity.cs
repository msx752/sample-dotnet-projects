using Samp.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Core.Entities
{
    [Table("Audits")]
    public class AuditEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// format is UserId+ActivityId
        /// </summary>
        [Required]
        public string Identifier { get; set; }

        [Required]
        public AuditType Type { get; set; }

        [Required]
        public string TableName { get; set; }

        [Required]
        public DateTimeOffset Timestamp { get; set; }

        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public string PrimaryKey { get; set; }
    }
}