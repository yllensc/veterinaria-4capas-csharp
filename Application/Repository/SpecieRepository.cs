using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class SpecieRepository: GenericRepository<Specie>, ISpecieRepository
{
    private readonly VeterinaryDbContext _context;

    public SpecieRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }

    public async Task<Specie> GetPets(string specie)
    {
        var petsBySpecie =  await _context.Species
                        .Include(s => s.Pets)
                        .Where(s => s.Name.ToLower().Equals(specie.ToLower()))
                        .FirstOrDefaultAsync();
        return petsBySpecie;
    }
}