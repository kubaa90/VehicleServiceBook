using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Context;
using VehicleServiceBook.Models;
using VehicleServiceBook.Services.Interfaces;

namespace VehicleServiceBook.Services
{
    public class InternalServiceService:IInternalServiceService
    {
        private readonly VehicleServiceContext _context;

        public InternalServiceService(VehicleServiceContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAsync(InternalServiceModel model)
        {
            await _context.InternalServices.AddAsync(model);
            return _context.SaveChanges() > 0;
        }
        //public bool Create(InternalServiceModel internalService)
        //{
        //    _context.InternalServices.Add(internalService);
        //    return _context.SaveChanges() > 0;
        //}
        public async Task<InternalServiceModel> GetAsync(int id)
        {
            return await _context.InternalServices.Include(d => d.Fault).ThenInclude(v=>v.Vehicle).SingleOrDefaultAsync(g => g.Id == id);
        }
        public async Task<IList<InternalServiceModel>> GetAllAsync()
        {
            var internalServices = await _context.InternalServices.Include(f => f.Fault).ThenInclude(v => v.Vehicle).ToListAsync();
            foreach (var item in internalServices)
            {
                item.Fault.AddDateTimeString = ConvertAddDateTimeToString(item.Fault.AddDateTime);
                item.Fault.ProcessDateTimeString = ConvertProcessTimeToString(item.Fault.ProcessDateTime);
                item.Fault.CloseDateTimeString = ConvertClosedTimeToString(item.Fault.CloseDateTime);
            }
            return internalServices;
        }
        public bool Update(InternalServiceModel service)
        {
            _context.InternalServices.Update(service);
            return _context.SaveChanges() > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var internalService = await _context.InternalServices.SingleOrDefaultAsync(d => d.Id == id);
            if (internalService == null)
                return false;

            _context.InternalServices.Remove(internalService);
            return _context.SaveChanges() > 0;
        }
        public string ConvertAddDateTimeToString(DateTime date)
        {
            return date.ToString("g");
        }

        public string ConvertProcessTimeToString(DateTime? date)
        {
            if (date != null)
            {
                return date?.ToString("g");
            }
            else
            {
                return "-";
            }
        }
        public string ConvertClosedTimeToString(DateTime? date)
        {
            if (date != null)
            {
                return date?.ToString("g");
            }
            else
            {
                return "Brak";
            }
        }

    }
}
