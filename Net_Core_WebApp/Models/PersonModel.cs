using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Net_Core_WebApp.Models
{
    public class PersonModel
    {
        [MinLength(1, ErrorMessage = "The first name field must be between 1 and 20 characters long.")]
        [MaxLength(20, ErrorMessage = "The first name field must be between 1 and 20 characters long.")]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name should not be empty")]
        public  string FirstName { get; set; }

        [MinLength(1, ErrorMessage = "The last name field must be between 1 and 20 characters long.")]
        [MaxLength(20, ErrorMessage = "The last name field must be between 1 and 20 characters long.")]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "First name should not be empty")]
        public  string LastName { get; set; }
    }
}
