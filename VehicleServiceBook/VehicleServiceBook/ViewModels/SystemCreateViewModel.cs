using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleServiceBook.ViewModels
{
    public class SystemCreateViewModel
    {
        [Display(Name = "Nazwa systemu")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Display(Name = "Opis systemu")]
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
