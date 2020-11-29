using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.ViewModels
{
    public class FaultProcessViewModel
    {
        public int FaultId { get; set; }
        [Display(Name = "Opis usterki")]
        public string Description { get; set; }
        public int VehicleId { get; set; }

        [Display(Name = "Dodał/a")]
        public string CreateUserName { get; set; }
#nullable enable
        public DateTime? AnalyzeDateTime { get; set; }
        [Display(Name = "Uwagi operatora")]
        public string? OperatorRemarks { get; set; }
        public string? Action { get; set; }
        public string? Status { get; set; }
        [Display(Name = "Czy pojazd jest dopuszczony do ruchu?")]
        public bool IsVehicleAbleToDrive { get; set; }
    }
}
