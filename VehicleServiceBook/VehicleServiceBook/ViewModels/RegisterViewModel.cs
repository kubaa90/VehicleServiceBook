using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VehicleServiceBook.ViewModels
{
    public class RegisterViewModel
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]

        public string Surname { get; set; }
        
        [Display(Name = "Numer telefonu")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email")]
        public string? Email { get; set; }
        [Display(Name = "Poziom uprawnień")]
        [Required(ErrorMessage = "Wybierz poziom uprawnień")]
        public string Role { get; set; }

        //public List<SelectListItem> RolesList { get; set; }
        /*public IEnumerable<SelectListItem> GetAllAvailableRolesList()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                new SelectListItem{ Value="Admin",Text="Admin"},
                new SelectListItem{ Value="User",Text="Obsługa"},
                new SelectListItem{ Value="Driver",Text="Kierowca"},
            };
            myList = data.ToList();
            return myList;
        }*/
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        public string RepeatPassword { get; set; }
    }
}
