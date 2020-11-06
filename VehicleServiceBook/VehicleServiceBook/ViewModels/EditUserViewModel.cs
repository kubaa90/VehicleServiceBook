using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleServiceBook.ViewModels
{
    public class EditUserViewModel
    {
        public string ID { get; set; }
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }
    }
}
