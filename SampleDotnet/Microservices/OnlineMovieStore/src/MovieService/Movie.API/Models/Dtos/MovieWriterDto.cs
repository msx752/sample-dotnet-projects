using Newtonsoft.Json;

namespace Samp.Movie.API.Models.Dtos
{
    public class MovieWriterDto
    {
        [JsonProperty("movieid")]
        public string MovieId { get; set; }

        //[JsonProperty("movie")]
        //public MovieDto Movie { get; set; }

        [JsonProperty("writerid")]
        public string WriterId { get; set; }

        [JsonProperty("writer")]
        public WriterDto Writer { get; set; }
    }
}