using System.ComponentModel.DataAnnotations;

namespace SampleProject.Identity.API.Models.Requests
{
    public class UserCreateModel
    {
        [Required]
        [StringLength(250)]
        public string Username { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Surname { get; set; }

        [StringLength(250)]
        public string Name { get; set; }
    }
}