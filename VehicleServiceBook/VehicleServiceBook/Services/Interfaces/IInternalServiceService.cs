using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.Services.Interfaces
{
    public interface IInternalServiceService
    {
        public Task<bool> CreateAsync(InternalServiceModel model);
        //public bool Create(InternalServiceModel model);
        public Task<InternalServiceModel> GetAsync(int id);
        public Task<IList<InternalServiceModel>> GetAllAsync();
        public bool Update(InternalServiceModel service);
        public Task<bool> DeleteAsync(int id);
    }
}
