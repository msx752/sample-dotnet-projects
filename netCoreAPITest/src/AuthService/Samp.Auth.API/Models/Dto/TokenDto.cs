namespace Samp.Auth.API.Models.Dto
{
    public class TokenDto
    {
        public string access_token { get; set; }
        public int ExpiresIn { get; set; }
        public string refresh_token { get; set; }
    }
}