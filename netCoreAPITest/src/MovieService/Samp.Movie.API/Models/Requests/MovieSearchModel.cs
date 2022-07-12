using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Samp.Movie.API.Models.Requests
{
    public class MovieSearchModel
    {
        [FromQuery]
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Query { get; set; }
    }
}