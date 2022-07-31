using Newtonsoft.Json;

namespace Samp.Movie.API.Models.Dtos
{
    public class CategoryDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; } 
    }
}
