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
        Task<bool> CreateAsync(ProducerModel producer);
        ProducerModel Get(int id);
        Task<ProducerModel> GetAsync(int id);
        IList<ProducerModel> GetAll();
        Task<IList<ProducerModel>> GetAllAsync();
        bool Update(ProducerModel producer);
        public bool CheckBeforeDelete(int id);
        bool Delete(int id);
    }
}
