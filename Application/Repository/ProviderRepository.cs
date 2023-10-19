using Domain.Entities;
using Domain.Interfaces;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository;
public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
{
    private readonly VeterinaryDbContext _context;

    public ProviderRepository(VeterinaryDbContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<(int totalRecords, IEnumerable<Provider> records)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Providers as IQueryable<Provider>;

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