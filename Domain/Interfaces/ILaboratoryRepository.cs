using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILaboratoryRepository : IGenericRepository<Laboratory>
    {
        Task<Laboratory> GetMedicines(string laboratory);
        Task<(int totalRecords, IEnumerable<Laboratory> records)> GetMedicines(string laboratory, int pageIndex, int pageSize, string search);

    }
}