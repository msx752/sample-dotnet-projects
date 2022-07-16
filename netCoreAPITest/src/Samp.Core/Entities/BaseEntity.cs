using Samp.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Samp.Core.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
        }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public Guid? CreatedBy { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}