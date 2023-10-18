using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class OwnerRepository: GenericRepository<Owner>, IOwnerRepository
{
    private readonly VeterinaryDbContext _context;

    public OwnerRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }

    public async Task<IEnumerable<Owner>> GetAllWithPetsAsync()
    {
        var OwnerWithPets = _context.Owners
                            .Include(o => o.Pets)
                            .ToListAsync();
        return await OwnerWithPets;
    }
}