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
        [Display(Name = "Dodał/a")]
        public string CreateUserName { get; set; }
        public DateTime AddDateTime { get; set; }

        #nullable enable
        public string? Action { get; set; }
        [Display(Name = "Ostatnio przetwarzał/a")]
        public string? ProcessedUserName { get; set; }
        public DateTime? ProcessDateTime { get; set; }
        public DateTime? CloseDateTime { get; set; }
        public string? Status { get; set; }
        [Display(Name = "Uwagi operatora")]
        public string? OperatorRemarks { get; set; }

        [NotMapped]
        [Display(Name = "Dodano")]
        public string? AddDateTimeString { get; set; }
        [NotMapped]
        [Display(Name = "Ostatnia akcja")]
        public string? ProcessDateTimeString { get; set; }
        [NotMapped]
        [Display(Name = "Zamknięto")]
        public string? CloseDateTimeString { get; set; }

    }
}
