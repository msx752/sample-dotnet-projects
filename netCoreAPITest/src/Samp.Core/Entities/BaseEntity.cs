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
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}