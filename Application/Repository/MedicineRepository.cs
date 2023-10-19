using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
{
    private readonly VeterinaryDbContext _context;

    public MedicineRepository(VeterinaryDbContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Medicine>> GetAllAsync()
    {
        return await _context.Medicines
            .Include(p => p.Laboratory)
            .Include(p => p.Provider)
            .ToListAsync();
    }
    public override async Task<(int totalRecords, IEnumerable<Medicine> records)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Medicines as IQueryable<Medicine>;

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
            .Include(p => p.Laboratory)
            .Include(p => p.Provider)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }

    public override async Task<Medicine> GetByIdAsync(int id)
    {
        return await _context.Medicines
        .Include(p => p.Laboratory)
        .Include(p => p.Provider)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Medicine>> GetUnderCant(int cant)
    {
        return await _context.Medicines
                    .Include(d => d.Provider)
                    .Where(m => m.QuantityDisp < cant)
                    .ToListAsync();
    }
    public async Task<IEnumerable<Medicine>> GetProvidersWithXMedicine(string medicine)
    {
        var providers = await _context.Medicines
                        .Include(m => m.Provider)
                        .Where(p => p.Name.ToLower().Equals(medicine.ToLower()))
                        .ToListAsync();
        return providers;

    }

    public async Task<(int totalRecords, IEnumerable<Medicine> records)> GetUnderCant(int cant, int pageIndex, int pageSize, string search)
    {
        var query = _context.Medicines
            .Include(d => d.Provider)
            .Where(m => m.QuantityDisp < cant);

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.Contains(search));
        }

        query = query.OrderBy(m => m.Id);

        var totalRecords = await query.CountAsync();
        var records = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }


    public async Task<(int totalRecords, IEnumerable<Medicine> records)> GetProvidersWithXMedicine(string medicine, int pageIndex, int pageSize, string search)
    {
        var query = _context.Medicines as IQueryable<Medicine>;

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Provider.Name.Contains(search));
        }
        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var providers = await _context.Medicines
                        .Include(m => m.Provider)
                        .Where(p => p.Name.ToLower().Equals(medicine.ToLower()))
                        .ToListAsync();
        return (totalRecords, providers);
    }
}