using Newtonsoft.Json;

namespace Samp.Movie.API.Models.Dtos
{
    public class MovieCategoryDto
    {
        [JsonProperty("movieid")]
        public string MovieId { get; set; }

        //[JsonProperty("movie")]
        //public MovieDto Movie { get; set; }

        [JsonProperty("categoryid")]
        public int CategoryId { get; set; }

        [JsonProperty("category")]
        public CategoryDto Category { get; set; }
    }
}