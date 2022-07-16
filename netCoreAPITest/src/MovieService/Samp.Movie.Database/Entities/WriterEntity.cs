﻿using Samp.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Movie.Database.Entities
{
    [Table("WriterEntity")]
    public class WriterEntity : BaseEntity
    {
        public WriterEntity()
        {
            MovieWriters = new HashSet<MovieWriterEntity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string FullName { get; set; }
        public virtual ICollection<MovieWriterEntity> MovieWriters { get; set; }
    }
}