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
        public async Task<bool> CreateAsync(VehicleModel vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            return _context.SaveChanges() > 0;
        }

        public async Task<VehicleModel> GetAsync(int id)
        {
            return await _context.Vehicles.Include(d => d.Producer).SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IList<VehicleModel>> GetAllAsync()
        {
            return await _context.Vehicles.Include(d => d.Producer).ToListAsync();
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

        public async Task<bool> DeleteAsync(int id)
        {
            var vehicle = await _context.Vehicles.SingleOrDefaultAsync(d => d.Id == id);
            if (vehicle == null)
                return false;

            _context.Vehicles.Remove(vehicle);
            return _context.SaveChanges() > 0;
        }
    }
}
