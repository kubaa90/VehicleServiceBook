using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> CreateAsync(ProducerModel producer)
        {
            await _context.Producers.AddAsync(producer);
            return _context.SaveChanges() > 0;
        }
        public ProducerModel Get(int id)
        {
            return _context.Producers.SingleOrDefault(g => g.Id == id);
        }
        public async Task<ProducerModel> GetAsync(int id)
        {
            return await _context.Producers.SingleOrDefaultAsync(g => g.Id == id);
        }

        public IList<ProducerModel> GetAll()
        {
            return _context.Producers.ToList();
        }
        public async Task<IList<ProducerModel>> GetAllAsync()
        {
            return await _context.Producers.ToListAsync();
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
