using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.ViewModels
{
    public class FaultCreateViewModel
    {

        public int FaultId { get; set; }
        [Display(Name = "Opis usterki")]
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Display(Name = "Numer Pojazdu")]
        [Required]
        public int VehicleId { get; set; }

    }
}
