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
        Task<IEnumerable<Medicine>> GetProvidersWithXMedicine(string medicine);
    }
}