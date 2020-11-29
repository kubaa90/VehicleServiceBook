using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.ViewModels
{
    public class FaultDetailsViewModel:FaultCreateViewModel
    {
        public FaultModel Fault { get; set; }
        [Display(Name = "Dodano")]
        public string AddDateTimeString { get; set; }
        [Display(Name = "Ostatnia akcja")]
        public string ProcessDateTimeString { get; set; }
        [Display(Name = "Uwagi")]
        public string OperatorRemarks { get; set; }

    }
}
