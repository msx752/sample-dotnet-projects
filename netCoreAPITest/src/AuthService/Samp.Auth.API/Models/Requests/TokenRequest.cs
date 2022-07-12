using System.ComponentModel.DataAnnotations;

namespace Samp.Auth.API.Models.Requests
{
    public class TokenRequest
    {
        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}