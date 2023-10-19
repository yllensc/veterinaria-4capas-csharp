using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class VeterinarianRepository : GenericRepository<Veterinarian>, IVeterinarianRepository
{
    private readonly VeterinaryDbContext _context;

    public VeterinarianRepository(VeterinaryDbContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<(int totalRecords, IEnumerable<Veterinarian> records)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Veterinarians as IQueryable<Veterinarian>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }

    public async Task<IEnumerable<Veterinarian>> GetCardiovascularSurgeonAsync()
    {
        return await _context.Veterinarians
                            .Where(p => p.Specialty.ToLower() == "cirugía vascular")
                            .ToListAsync();
    }

    public async Task<(int totalRecords, IEnumerable<Veterinarian> records)> GetCardiovascularSurgeonAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Veterinarians as IQueryable<Veterinarian>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
            .Where(p => p.Specialty.ToLower() == "cirugía vascular")
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }
}