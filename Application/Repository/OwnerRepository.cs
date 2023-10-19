using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
{
    private readonly VeterinaryDbContext _context;

    public OwnerRepository(VeterinaryDbContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<(int totalRecords, IEnumerable<Owner> records)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Owners as IQueryable<Owner>;

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }
    public async Task<IEnumerable<Owner>> GetAllWithPetsAsync()
    {
        var OwnerWithPets = _context.Owners
                            .Include(o => o.Pets)
                            .ToListAsync();
        return await OwnerWithPets;
    }

    public async Task<(int totalRecords, IEnumerable<Owner> records)> GetAllWithPetsAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Owners.Include(o => o.Pets) as IQueryable<Owner>;

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(o => o.Name.Contains(search));
        }

        query = query.OrderBy(o => o.Id);

        var totalRecords = await query.CountAsync();

        var ownersWithPets = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, ownersWithPets);
    }

}