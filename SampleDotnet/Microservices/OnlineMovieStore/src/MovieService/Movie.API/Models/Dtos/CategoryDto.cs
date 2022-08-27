using Newtonsoft.Json;

namespace SampleProject.Movie.API.Models.Dtos
{
    public class CategoryDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}