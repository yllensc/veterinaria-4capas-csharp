using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class MedicineRepository: GenericRepository<Medicine>, IMedicineRepository
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

    public override async Task<Medicine> GetByIdAsync(int id)
    {
        return await _context.Medicines
        .Include(p => p.Laboratory)
        .Include(p => p.Provider)
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}