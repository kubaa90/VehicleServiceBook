using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.Services.Interfaces
{
    public interface IFaultService
    {
        bool Create(FaultModel vehicle);
        FaultModel Get(int id);

        IList<FaultModel> GetAll();

        bool Update(FaultModel vehicle);
        public bool CheckBeforeDelete(int id);
        bool Delete(int id);
    }
}
