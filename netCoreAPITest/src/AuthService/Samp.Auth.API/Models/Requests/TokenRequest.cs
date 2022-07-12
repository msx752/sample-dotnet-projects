using System.ComponentModel.DataAnnotations;

namespace Samp.Auth.API.Models.Requests
{
    public class TokenRequest
    {
        [StringLength(255)]
        public string Username { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string grant_type { get; set; }
    }
}