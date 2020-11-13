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
    public class VehicleService:IVehicleService
    {
        private readonly VehicleServiceContext _context;

        public VehicleService(VehicleServiceContext context)
        {
            _context = context;
        }
        public bool Create(VehicleModel vehicle)
        {
            _context.Vehicles.Add(vehicle);
            return _context.SaveChanges() > 0;
        }

        public VehicleModel Get(int id)
        {
            return _context.Vehicles.Include(d => d.Producer).SingleOrDefault(g => g.Id == id);
        }

        public IList<VehicleModel> GetAll()
        {
            return _context.Vehicles.Include(d => d.Producer).ToList();
        }

        public bool Update(VehicleModel vehicle)
        {
            _context.Vehicles.Update(vehicle);
            return _context.SaveChanges() > 0;
        }

        public bool CheckBeforeDelete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var vehicle = _context.Vehicles.SingleOrDefault(d => d.Id == id);
            if (vehicle == null)
                return false;

            _context.Vehicles.Remove(vehicle);
            return _context.SaveChanges() > 0;
        }
    }
}
