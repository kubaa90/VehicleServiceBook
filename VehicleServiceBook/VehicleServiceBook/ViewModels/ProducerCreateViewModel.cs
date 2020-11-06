using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleServiceBook.ViewModels
{
    public class ProducerCreateViewModel
    {
        [Display(Name = "Nazwa producenta")]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(300)]
        public string Address { get; set; }
    }
}
