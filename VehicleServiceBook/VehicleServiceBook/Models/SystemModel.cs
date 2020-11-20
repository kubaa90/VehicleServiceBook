using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VehicleServiceBook.Models
{
    public class SystemModel
    {
        [Key]
        public int Id { get; set; }
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
