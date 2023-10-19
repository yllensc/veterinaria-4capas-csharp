using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class LaboratoryRepository : GenericRepository<Laboratory>, ILaboratoryRepository
{
    private readonly VeterinaryDbContext _context;

    public LaboratoryRepository(VeterinaryDbContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Laboratory>> GetAllAsync()
    {
        return await _context.Laboratories
            .Include(p => p.Medicines)
            .ToListAsync();
    }

    public override async Task<Laboratory> GetByIdAsync(int id)
    {
        return await _context.Laboratories
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    public override async Task<(int totalRecords, IEnumerable<Laboratory> records)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Laboratories as IQueryable<Laboratory>;

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
    public async Task<Laboratory> GetMedicines(string laboratory)
    {
        var medicinesByLaboratory = _context.Laboratories
                                    .Include(l => l.Medicines)
                                    .Where(l => l.Name.ToLower() == laboratory.ToLower())
                                    .FirstOrDefaultAsync();
        return await medicinesByLaboratory;
    }
    public async Task<(int totalRecords, IEnumerable<Laboratory> records)> GetMedicines(string laboratory, int pageIndex, int pageSize, string search)
    {
        var query = _context.Laboratories as IQueryable<Laboratory>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
            .Include(l => l.Medicines)
            .Where(l => l.Name.ToLower() == laboratory.ToLower())
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }
}