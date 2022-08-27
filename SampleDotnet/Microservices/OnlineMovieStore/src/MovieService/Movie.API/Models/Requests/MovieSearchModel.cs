using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SampleProject.Movie.API.Models.Requests
{
    public class MovieSearchModel
    {
        [FromQuery]
        [Required]
        [StringLength(255, MinimumLength = 2)]
        [JsonProperty("query")]
        public string Query { get; set; }
    }
}