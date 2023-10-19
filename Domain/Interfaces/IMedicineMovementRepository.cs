using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMedicineMovementRepository : IGenericRepository<MedicineMovement>
    {
        Task<string> RegisterAsync(MedicineMovement model);
        Task<IEnumerable<object>> GetListMovements();
        Task<(int totalRecords, IEnumerable<object> records)> GetListMovements(int pageIndex, int pageSize, string search);



    }
}