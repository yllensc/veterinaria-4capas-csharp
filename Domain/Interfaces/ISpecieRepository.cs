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
        Task<(int totalRecords, IEnumerable<Specie> records)> GetPets(string specie, int pageIndex, int pageSize, string search);
        Task<IEnumerable<Specie>> GetPetsInGroups();
        Task<(int totalRecords, IEnumerable<Specie> records)> GetPetsInGroups(int pageIndex, int pageSize, string search);

    }
}