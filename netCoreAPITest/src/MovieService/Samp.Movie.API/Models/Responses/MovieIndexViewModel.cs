using Newtonsoft.Json;
using Samp.Movie.API.Models.Dtos;

namespace Samp.Movie.API.Models.Responses
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