using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        Task<IEnumerable<Owner>> GetAllWithPetsAsync();
        Task<(int totalRecords, IEnumerable<Owner> records)> GetAllWithPetsAsync(int pageIndex, int pageSize, string search);

    }
}