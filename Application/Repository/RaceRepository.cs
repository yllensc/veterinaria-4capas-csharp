using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class RaceRepository: GenericRepository<Race>, IRaceRepository
{
    private readonly VeterinaryDbContext _context;

    public RaceRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
    public override async Task<IEnumerable<Race>> GetAllAsync()
    {
        return await _context.Races
            .Include(p => p.Specie)
            .ToListAsync();
    }

    public override async Task<Race> GetByIdAsync(int id)
    {
        return await _context.Races
        .Include(p => p.Specie)
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}