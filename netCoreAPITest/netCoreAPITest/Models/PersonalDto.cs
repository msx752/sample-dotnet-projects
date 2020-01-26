using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netCoreAPITest.Models
{
    
    public class PersonalDto
    {
        //NO ID PROPERTY HERE

        [Required]
        public byte Age { get; set; }

        //attributes for model validation while doing post/delete etc...
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
    }
}
