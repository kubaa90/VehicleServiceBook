using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VehicleServiceBook.ViewModels
{
    public class VehicleCreateViewModel
    {
        [Display(Name = "Numer Pojazdu")]
        [Required]
        [MaxLength(30)]
        public string Number { get; set; }
        [Display(Name = "Numer VIN")]
        [Required]
        [MaxLength(50)]
        public string VIN { get; set; }
        [Required]
        [MaxLength(10)]
        public string PlateNumber { get; set; }
        public IEnumerable<SelectListItem> ProducersList { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}
