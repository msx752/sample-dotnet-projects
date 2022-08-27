﻿using Samp.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samp.Movie.Database.Entities
{
    [Table("MovieWriterEntity")]
    public class MovieWriterEntity : BaseEntity
    {
        public string MovieId { get; set; }
        public virtual MovieEntity Movie { get; set; }
        public string WriterId { get; set; }
        public virtual WriterEntity Writer { get; set; }
    }
}