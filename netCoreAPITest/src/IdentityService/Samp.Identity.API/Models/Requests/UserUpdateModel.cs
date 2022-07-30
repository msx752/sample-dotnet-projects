using System.ComponentModel.DataAnnotations;

namespace Samp.Identity.API.Models.Requests
{
    public class UserUpdateModel
    {
        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Surname { get; set; }

        [StringLength(250)]
        public string Name { get; set; }
    }
}