using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Net_Core_WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace Net_Core_WebApp.ViewModel
{
    public class PersonViewModel
    {
        [Required]
        public PersonModel person { get; set; }

        [ValidateNever]
        public SuccessErrorMessageModel successErrorMessage { get; set; } = new();
    }


}
