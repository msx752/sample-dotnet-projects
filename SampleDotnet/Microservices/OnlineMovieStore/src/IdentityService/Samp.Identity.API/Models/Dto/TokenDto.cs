using Newtonsoft.Json;

namespace Samp.Identity.API.Models.Dto
{
    public class TokenDto
    {
        [JsonProperty("access_token")]
        public string access_token { get; set; }

        [JsonProperty("expiresat")]
        public DateTime ExpiresAt { get; set; }

        [JsonProperty("refresh_token")]
        public string refresh_token { get; set; }

        [JsonProperty("refreshtokenexpiresat")]
        public DateTime RefreshTokenExpiresAt { get; set; }

        [JsonProperty("user")]
        public UserDto User { get; set; }
    }
}