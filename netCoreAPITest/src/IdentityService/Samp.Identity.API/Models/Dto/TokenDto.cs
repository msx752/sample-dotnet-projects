namespace Samp.Identity.API.Models.Dto
{
    public class TokenDto
    {
        public string access_token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string refresh_token { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }
    }
}