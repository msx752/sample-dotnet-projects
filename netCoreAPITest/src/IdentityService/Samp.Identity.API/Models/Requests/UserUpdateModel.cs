using System.ComponentModel.DataAnnotations;

namespace Samp.Identity.API.Models.Requests
{
    public class UserUpdateModel
    {
        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [Required]
        [StringLength(250)]
        public string Surname { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
    }
}