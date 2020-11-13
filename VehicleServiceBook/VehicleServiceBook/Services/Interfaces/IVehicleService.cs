using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.Services.Interfaces
{
    public interface IVehicleService
    {
        bool Create(VehicleModel vehicle);
        VehicleModel Get(int id);

        IList<VehicleModel> GetAll();

        bool Update(VehicleModel vehicle);
        public bool CheckBeforeDelete(int id);
        bool Delete(int id);
    }
}
