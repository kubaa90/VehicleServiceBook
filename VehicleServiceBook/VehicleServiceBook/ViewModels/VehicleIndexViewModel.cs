using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.ViewModels
{
    public class VehicleIndexViewModel
    {
        public IList<VehicleModel> Vehicles { get; set; }

        //public string IsOnWarrantyString { get; set; }

        //public VehicleIndexViewModel()
        //{
        //    foreach (VehicleModel vehicle in Vehicles)
        //    {
        //        IsOnWarrantyString = vehicle.IsOnWarranty ? "TAK" : "NIE";
        //    }
        //}

    }
}
