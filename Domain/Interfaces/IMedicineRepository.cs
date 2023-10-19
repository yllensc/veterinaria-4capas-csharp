using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        Task<IEnumerable<Medicine>> GetUnderCant(int cant);
        Task<(int totalRecords, IEnumerable<Medicine> records)> GetUnderCant(int cant, int pageIndex, int pageSize, string search);
        Task<IEnumerable<Medicine>> GetProvidersWithXMedicine(string medicine);
        Task<(int totalRecords, IEnumerable<Medicine> records)> GetProvidersWithXMedicine(string medicine, int pageIndex, int pageSize, string search);

    }
}