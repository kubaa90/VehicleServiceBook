using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.ViewModels
{
    public class VehicleCreateViewModel
    {
        public VehicleModel Vehicle { get; set; } 
    }
}
