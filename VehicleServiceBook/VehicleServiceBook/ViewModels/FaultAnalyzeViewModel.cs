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
    public class FaultAnalyzeViewModel:FaultEditViewModel
    {
        public string AddDateTimeString { get; set; }
        [StringLength(450)]
        public IdentityUser IdentityUser { get; set; }
        #nullable enable
        public string? Action { get; set; }
        public string? Status { get; set; }
    }
}
