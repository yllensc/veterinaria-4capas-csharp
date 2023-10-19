using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class SpecieRepository : GenericRepository<Specie>, ISpecieRepository
{
    private readonly VeterinaryDbContext _context;

    public SpecieRepository(VeterinaryDbContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<(int totalRecords, IEnumerable<Specie> records)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Species as IQueryable<Specie>;

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }
    public async Task<Specie> GetPets(string specie)
    {
        var petsBySpecie = await _context.Species
                        .Include(s => s.Pets)
                        .Where(s => s.Name.ToLower().Equals(specie.ToLower()))
                        .FirstOrDefaultAsync();
        return petsBySpecie;
    }
    public async Task<(int totalRecords, IEnumerable<Specie> records)> GetPets(string specie, int pageIndex, int pageSize, string search)
    {
        var query = _context.Species as IQueryable<Specie>;

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
                        .Include(s => s.Pets)
                        .Where(s => s.Name.ToLower().Equals(specie.ToLower()))
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }
    public async Task<IEnumerable<Specie>> GetPetsInGroups()
    {
        var species = await _context.Species
                    .Include(s => s.Pets)
                    .Include(s => s.Races)
                    .ToListAsync();
        return species;
    }

    public async Task<(int totalRecords, IEnumerable<Specie> records)> GetPetsInGroups(int pageIndex, int pageSize, string search)
    {
        var query = _context.Species as IQueryable<Specie>;

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var species = await _context.Species
                    .Include(s => s.Pets)
                    .Include(s => s.Races)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, species);
    }
}