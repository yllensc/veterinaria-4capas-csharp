using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class RaceRepository : GenericRepository<Race>, IRaceRepository
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
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    public override async Task<(int totalRecords, IEnumerable<Race> records)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Races as IQueryable<Race>;

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
            .Include(p => p.Specie)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }
}