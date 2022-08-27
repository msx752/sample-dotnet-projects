using Newtonsoft.Json;

namespace Samp.Movie.API.Models.Dtos
{
    public class MovieDirectorDto
    {
        [JsonProperty("movieid")]
        public string MovieId { get; set; }

        //[JsonProperty("movie")]
        //public MovieDto Movie { get; set; }

        [JsonProperty("directorid")]
        public string DirectorId { get; set; }

        [JsonProperty("director")]
        public DirectorDto Director { get; set; }
    }
}