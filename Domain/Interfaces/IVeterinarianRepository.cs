using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IVeterinarianRepository : IGenericRepository<Veterinarian>
    {
        Task<IEnumerable<Veterinarian>> GetCardiovascularSurgeonAsync();
        Task<(int totalRecords, IEnumerable<Veterinarian> records)> GetCardiovascularSurgeonAsync(int pageIndex, int pageSize, string search);

    }
}