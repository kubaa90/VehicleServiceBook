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
        SystemModel Get(int id);

        IList<SystemModel> GetAll();

        bool Update(SystemModel system);
        public bool CheckBeforeDelete(int id);
        bool Delete(int id);
    }
}
