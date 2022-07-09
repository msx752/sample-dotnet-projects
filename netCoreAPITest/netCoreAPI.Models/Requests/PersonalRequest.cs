using System.ComponentModel.DataAnnotations;

namespace netCoreAPI.Models.Requests
{
    public class PersonalRequest
    {
        [Required]
        public byte Age { get; set; }

        //attributes for model validation while doing post/delete etc...
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string NationalId { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
    }
}