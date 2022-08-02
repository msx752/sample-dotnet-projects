using Newtonsoft.Json;

namespace Samp.Movie.API.Models.Dtos
{
    public class DirectorDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("fullname")]
        public string FullName { get; set; }
    }
}