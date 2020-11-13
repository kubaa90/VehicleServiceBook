using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.Services.Interfaces
{
    public interface IProducerService
    {
        bool Create(ProducerModel producer);
        ProducerModel Get(int id);

        IList<ProducerModel> GetAll();

        bool Update(ProducerModel producer);
        public bool CheckBeforeDelete(int id);
        bool Delete(int id);
    }
}
