using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleServiceBook.ViewModels
{
    public class LoginViewModel
        {
            [Required]
            [Display(Name = "Imię")]
        public string UserName { get; set; }
            [Required]
            [Display(Name = "Hasło")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
