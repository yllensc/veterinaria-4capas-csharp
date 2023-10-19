using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class TreatmentRepository : GenericRepository<Treatment>, ITreatmentRepository
{
    private readonly VeterinaryDbContext _context;

    public TreatmentRepository(VeterinaryDbContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Treatment>> GetAllAsync()
    {
        return await _context.Treatments
            .Include(p => p.Appointment)
            .Include(p => p.Medicine)
            .ToListAsync();
    }

    public override async Task<Treatment> GetByIdAsync(int id)
    {
        return await _context.Treatments
        .Include(p => p.Appointment)
        .Include(p => p.Medicine)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    public override async Task<(int totalRecords, IEnumerable<Treatment> records)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Treatments as IQueryable<Treatment>;

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Observations.Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
            .Include(p => p.Appointment)
            .Include(p => p.Medicine)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }
}