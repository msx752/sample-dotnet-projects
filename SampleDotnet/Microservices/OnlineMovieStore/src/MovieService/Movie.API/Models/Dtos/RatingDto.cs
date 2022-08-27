using Newtonsoft.Json;

namespace SampleProject.Movie.API.Models.Dtos
{
    public class RatingDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("averagerating")]
        public double AverageRating { get; set; }

        [JsonProperty("numvotes")]
        public int NumVotes { get; set; }

        [JsonProperty("movieid")]
        public string MovieId { get; set; }
    }
}