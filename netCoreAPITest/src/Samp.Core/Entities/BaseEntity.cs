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
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}