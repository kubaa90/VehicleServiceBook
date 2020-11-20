using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleServiceBook.Context;
using VehicleServiceBook.Models;
using VehicleServiceBook.Services.Interfaces;

namespace VehicleServiceBook.Services
{
    public class SystemService:ISystemService
    {
        private readonly VehicleServiceContext _context;

        public SystemService(VehicleServiceContext context)
        {
            _context = context;
        }
        public bool Create(SystemModel system)
        {
            _context.Systems.Add(system);
            return _context.SaveChanges() > 0;
        }

        public SystemModel Get(int id)
        {
            return _context.Systems.SingleOrDefault(g => g.Id == id);
        }

        public IList<SystemModel> GetAll()
        {
            return _context.Systems.ToList();
        }

        public bool Update(SystemModel system)
        {
            _context.Systems.Update(system);
            return _context.SaveChanges() > 0;
        }

        public bool CheckBeforeDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var system = _context.Systems.SingleOrDefault(d => d.Id == id);
            if (system == null)
                return false;

            _context.Systems.Remove(system);
            return _context.SaveChanges() > 0;
        }
    }
}
