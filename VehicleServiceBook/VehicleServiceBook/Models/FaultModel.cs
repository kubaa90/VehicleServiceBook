using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VehicleServiceBook.Models
{
    public class FaultModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Opis usterki")]
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Display(Name = "Numer Pojazdu")]
        [Required]
        public int VehicleId { get; set; }

        [ForeignKey("VehicleId")]
        public VehicleModel Vehicle { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser IdentityUser { get; set; }
        public DateTime AddDateTime { get; set; }

        #nullable enable
        public string? Action { get; set; }
        public DateTime? AnalyzeDateTime { get; set; }
        public DateTime? ClosedActionDateTime { get; set; }
        public string? Status { get; set; }
        

    }
}
