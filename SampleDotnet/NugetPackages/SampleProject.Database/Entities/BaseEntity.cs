using SampleProject.Core.Interfaces.DbContexts;
using System;
using System.ComponentModel.DataAnnotations;

namespace SampleProject.Core.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}