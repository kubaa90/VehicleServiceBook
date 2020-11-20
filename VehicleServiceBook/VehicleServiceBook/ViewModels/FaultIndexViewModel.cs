using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.ViewModels
{
    public class FaultIndexViewModel
    {
        public IList<FaultModel> Faults { get; set; }
    }
}
