using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleServiceBook.Context;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.Services.Interfaces
{
    public class FaultService:IFaultService
    {
        private readonly VehicleServiceContext _context;

        public FaultService(VehicleServiceContext context)
        {
            _context = context;
        }
        public bool Create(FaultModel fault)
        {
            _context.Faults.Add(fault);
            return _context.SaveChanges() > 0;
        }

        public FaultModel Get(int id)
        {
            return _context.Faults.Include(d => d.Vehicle).SingleOrDefault(g => g.Id == id);
        }

        public IList<FaultModel> GetAll()
        {
            return _context.Faults.Include(e => e.IdentityUser).Include(d => d.Vehicle).ToList();
        }

        public bool Update(FaultModel fault)
        {
            _context.Faults.Update(fault);
            return _context.SaveChanges() > 0;
        }

        public bool CheckBeforeDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var fault = _context.Faults.SingleOrDefault(d => d.Id == id);
            if (fault == null)
                return false;

            _context.Faults.Remove(fault);
            return _context.SaveChanges() > 0;
        }
    }
}
