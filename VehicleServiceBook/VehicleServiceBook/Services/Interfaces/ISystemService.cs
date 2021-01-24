using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.Services.Interfaces
{
    public interface ISystemService
    {
        bool Create(SystemModel system);
        Task<bool> CreateAsync(SystemModel system);
        SystemModel Get(int id);
        Task<SystemModel> GetAsync(int id);

        IList<SystemModel> GetAll();
        Task<IList<SystemModel>> GetAllAsync(int id);

        bool Update(SystemModel system);
        public bool CheckBeforeDelete(int id);
        bool Delete(int id);
        Task<bool> DeleteAsync(int id);
    }
}
