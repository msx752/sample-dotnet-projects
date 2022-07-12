using System.ComponentModel.DataAnnotations;

namespace Samp.Auth.API.Models.Requests
{
    public class TokenRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}