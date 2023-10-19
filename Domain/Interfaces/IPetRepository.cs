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
        Task<IEnumerable<object>> GetPetsByRace();
    }
}