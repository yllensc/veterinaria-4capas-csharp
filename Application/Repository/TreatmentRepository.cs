using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class TreatmentRepository: GenericRepository<Treatment>, ITreatmentRepository
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
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}