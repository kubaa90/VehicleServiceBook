using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;

namespace VehicleServiceBook.Services.Interfaces
{
    public interface IFaultService
    {
        bool Create(FaultModel vehicle);
        FaultModel Get(int id);
        public Task<FaultModel> GetAsync(int id);

        IList<FaultModel> GetAll();
        public Task<IList<FaultModel>> GetAllAsync();
        bool Update(FaultModel vehicle);
        public bool CheckBeforeDelete(int id);
        bool Delete(int id);
        public Task DeleteAsync(int id);
        string ProcessStatus(string action);
        public string ConvertAddDateTimeToString(DateTime date);
        public string ConvertProcessTimeToString(DateTime? date);
        public string ConvertClosedTimeToString(DateTime? date);
        public string ProcessRemarks(string remarks);
        public string ReverseProcessRemarks(string remarks);

    }
}
