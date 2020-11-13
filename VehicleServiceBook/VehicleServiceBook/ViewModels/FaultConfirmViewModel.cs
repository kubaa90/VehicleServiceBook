using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.ViewModels
{
    public class FaultConfirmViewModel:FaultCreateViewModel
    {
        public int Id { get; set; }

        public VehicleModel Vehicle { get; set; }
    }
}
