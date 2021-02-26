using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleServiceBook.Models
{
    public class InternalServiceModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Numer Usterki")]
        [Required]
        public int FaultId { get; set; }

        [ForeignKey("FaultId")]
        public FaultModel Fault { get; set; }
        //public int VehicleNumber { get; set; }
        [Display(Name ="Oględziny")]
        public int InspectionStatus { get; set; }
#nullable enable
        [Display(Name = "Numer Systemu")]
        public int? SystemId { get; set; }

        [ForeignKey("SystemId")]
        public SystemModel? System { get; set; }

        [Display(Name = "Uwagi operatora serwisu")]
        public string? ServiceOperatorRemarks { get; set; }
    }
}
