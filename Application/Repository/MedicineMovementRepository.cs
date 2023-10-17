using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class MedicineMovementRepository: GenericRepository<MedicineMovement>, IMedicineMovementRepository
{
    private readonly VeterinaryDbContext _context;

    public MedicineMovementRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
    public override async Task<IEnumerable<MedicineMovement>> GetAllAsync()
    {
        return await _context.MedicineMovements
            .Include(p => p.TypeMovement)
            .ToListAsync();
    }

    public override async Task<MedicineMovement> GetByIdAsync(int id)
    {
        return await _context.MedicineMovements
        .Include(p => p.TypeMovement)
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}