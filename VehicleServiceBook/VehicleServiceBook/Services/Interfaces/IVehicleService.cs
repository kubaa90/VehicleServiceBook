using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<bool> CreateAsync(VehicleModel vehicle);
        Task<VehicleModel> GetAsync(int id);

        Task<IList<VehicleModel>> GetAllAsync();

        bool Update(VehicleModel vehicle);
        public bool CheckBeforeDelete(int id);
        Task<bool> DeleteAsync(int id);
    }
}
