using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class LaboratoryRepository: GenericRepository<Laboratory>, ILaboratoryRepository
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
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}