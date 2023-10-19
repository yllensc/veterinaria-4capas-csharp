using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPetRepository : IGenericRepository<Pet>
    {
        Task<string> RegisterAsync(Pet model);
        Task<IEnumerable<Pet>> GetPetsWithXRace(string race);
        Task<(int totalRecords, IEnumerable<Pet> records)> GetPetsWithXRace(string race, int pageIndex, int pageSize, string search);
        Task<IEnumerable<object>> GetPetsByRace();
        Task<(int totalRecords, IEnumerable<object> records)> GetPetsByRace(int pageIndex, int pageSize, string search);

    }
}