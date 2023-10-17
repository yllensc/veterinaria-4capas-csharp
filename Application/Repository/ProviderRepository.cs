using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class ProviderRepository: GenericRepository<Provider>, IProviderRepository
{
    private readonly VeterinaryDbContext _context;

    public ProviderRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
    
}