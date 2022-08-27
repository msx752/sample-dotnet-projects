using Newtonsoft.Json;
using SampleProject.Movie.Database.Enums;

namespace SampleProject.Movie.API.Models.Dtos
{
    public class MovieDto
    {
        [JsonProperty("categories")]
        public List<MovieCategoryDto> Categories { get; set; }

        [JsonProperty("moviedirectors")]
        public List<MovieDirectorDto> MovieDirectors { get; set; }

        [JsonProperty("moviewriters")]
        public List<MovieWriterDto> MovieWriters { get; set; }

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