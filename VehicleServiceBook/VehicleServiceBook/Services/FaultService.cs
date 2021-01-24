using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleServiceBook.Context;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.Services.Interfaces
{
    public class FaultService : IFaultService
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
        public async Task<bool> CreateAsync(FaultModel fault)
        {
            await _context.Faults.AddAsync(fault);
            return _context.SaveChanges() > 0;
        }
        public FaultModel Get(int id)
        {
            return _context.Faults.Include(d => d.Vehicle).SingleOrDefault(g => g.Id == id);
        }
        public async Task<FaultModel> GetAsync(int id)
        {
            return await _context.Faults.Include(d => d.Vehicle).SingleOrDefaultAsync(g => g.Id == id);
        }

        public IList<FaultModel> GetAll()
        {
            var faults = _context.Faults.
                Include(d => d.Vehicle).ToList();
            foreach (var item in faults)
            {
                item.AddDateTimeString = ConvertAddDateTimeToString(item.AddDateTime);
                item.ProcessDateTimeString = ConvertProcessTimeToString(item.ProcessDateTime);
                item.CloseDateTimeString = ConvertClosedTimeToString(item.CloseDateTime);
            }
            return faults;
        }
        public async Task<IList<FaultModel>> GetAllAsync()
        {
            var faults = await _context.Faults.Include(d => d.Vehicle).ToListAsync();
            foreach (var item in faults)
            {
                item.AddDateTimeString = ConvertAddDateTimeToString(item.AddDateTime);
                item.ProcessDateTimeString = ConvertProcessTimeToString(item.ProcessDateTime);
                item.CloseDateTimeString = ConvertClosedTimeToString(item.CloseDateTime);
            }
            return faults;
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
        public async Task<bool> DeleteAsync(int id)
        {
            var fault = await _context.Faults.SingleOrDefaultAsync(d => d.Id == id);
            if (fault == null)
                return false;

            _context.Faults.Remove(fault);
            return _context.SaveChanges() > 0;
        }
        public string ProcessStatus(string action)
        {
            string status = string.Empty;
            switch (action)
            {
                case "":
                    status = "Nowe";
                    break;
                case "IntService":
                    status = "Przekazano do serwisu wewnętrznego";
                    break;
                case "ExtService":
                    status = "Zgłoszono do serwisu zewnętrznego";
                    break;
                case "Warranty":
                    status = "Zgłoszono do producenta pojazdu (naprawa gwarancyjna)";
                    break;
                case "Ignore":
                    status = "Zignorowana";
                    break;
                case "Closed":
                    status = "Zamknięta";
                    break;

            }
            return status;
        }
        public string ProcessRemarks(string remarks)
        {
            string processedRemarks = string.Empty;
            if (string.IsNullOrWhiteSpace(remarks))
            {
                processedRemarks = "-";
            }
            return remarks;
        }
        public string ReverseProcessRemarks(string remarks)
        {
            string processedRemarks = string.Empty;
            if (remarks=="-")
            {
                return processedRemarks;
            }
            else
            {
                processedRemarks = remarks;
                return processedRemarks;
            }
        }
    }
}
