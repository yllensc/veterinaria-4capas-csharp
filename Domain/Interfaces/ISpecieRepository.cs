using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISpecieRepository : IGenericRepository<Specie>
    {
        Task<Specie> GetPets(string specie);
        Task<IEnumerable<Specie>> GetPetsInGroups();
    }
}