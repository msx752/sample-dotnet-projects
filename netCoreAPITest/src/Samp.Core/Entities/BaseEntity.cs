using Samp.Core.Interfaces;
using System;

namespace Samp.Core.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
        }

        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}