using Newtonsoft.Json;
using Samp.Movie.Database.Enums;

namespace Samp.Movie.API.Models.Dtos
{
    public class MovieDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("runtimeminutes")]
        public int RuntimeMinutes { get; set; }

        [JsonProperty("startyear")]
        public int StartYear { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rating")]
        public RatingDto Rating { get; set; }

        [JsonProperty("type")]
        public MovieType Type { get; set; }

        [JsonProperty("usdprice")]
        public decimal UsdPrice { get; set; }

        [JsonProperty("itemdatabase")]
        public string ItemDatabase { get; set; }
    }
}