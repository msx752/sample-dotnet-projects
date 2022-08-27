using Newtonsoft.Json;
using SampleProject.Movie.API.Models.Dtos;

namespace SampleProject.Movie.API.Models.Responses
{
    public class MovieIndexViewModel
    {
        public MovieIndexViewModel()
        {
            HighRatings = new();
            All = new();
            RecentlyAdded = new();
        }

        [JsonProperty("highratings")]
        public List<MovieDto> HighRatings { get; set; }

        [JsonProperty("all")]
        public List<MovieDto> All { get; set; }

        [JsonProperty("recentlyadded")]
        public List<MovieDto> RecentlyAdded { get; set; }
    }
}