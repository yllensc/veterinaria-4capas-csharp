using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class TypeMovementRepository : GenericRepository<TypeMovement>, ITypeMovementRepository
{
    private readonly VeterinaryDbContext _context;

    public TypeMovementRepository(VeterinaryDbContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<(int totalRecords, IEnumerable<TypeMovement> records)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.TypeMovements as IQueryable<TypeMovement>;

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
}