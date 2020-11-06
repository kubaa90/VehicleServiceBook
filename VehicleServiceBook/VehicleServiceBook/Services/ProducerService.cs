using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Context;
using VehicleServiceBook.Models;
using VehicleServiceBook.Services.Interfaces;

namespace VehicleServiceBook.Services
{
    public class ProducerService:IProducerService
    {
        private readonly VehicleServiceContext _context;

        public ProducerService(VehicleServiceContext context)
        {
            _context = context;
        }
        public bool Create(ProducerModel producer)
        {
            _context.Producers.Add(producer);
            return _context.SaveChanges() > 0;
        }

        public ProducerModel Get(int id)
        {
            return _context.Producers.SingleOrDefault(g => g.Id == id);
        }

        public IList<ProducerModel> GetAll()
        {
            return _context.Producers.ToList();
        }

        public bool Update(ProducerModel producer)
        {
            _context.Producers.Update(producer);
            return _context.SaveChanges() > 0;
        }

        public bool CheckBeforeDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var producer = _context.Producers.SingleOrDefault(d => d.Id == id);
            if (producer == null)
                return false;

            _context.Producers.Remove(producer);
            return _context.SaveChanges() > 0;
        }
    }
}
