using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleServiceBook.Models
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
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
        [Required]
        public int ProducerId { get; set; }

        [ForeignKey("ProducerId")]
        public ProducerModel Producer { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }

        public string RegistrationDateString { get; set; }

        public bool HasFault { get; set; }

#nullable enable
        [Display(Name = "Wyłączony z ruchu")]
        public bool? IsAbleToDrive { get; set; }
    }
}
